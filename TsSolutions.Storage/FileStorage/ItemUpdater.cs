using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;
using System.Timers;

namespace TsSolutions.Storage.FileStorage
{
    public class ItemUpdater<T>
    {
        private System.Timers.Timer countDown = new System.Timers.Timer(1700);

        private readonly object _lock = new object();

        private const string UpdateFileName = "updateInfo.pfupd";
        private CancellationTokenSource _cts;

        public delegate void UpdateDelegate(ItemUpdateInfoDto updateInfo);

        internal event UpdateDelegate ContentChange;

        public List<string> FolderPaths { get; set; }

        public int DelayMs { get; set; }

        public ItemUpdater()
        {
            DelayMs = 5000;
            countDown = new System.Timers.Timer(DelayMs);
            this._cts = new CancellationTokenSource();
            countDown.Elapsed += CountDown_Elapsed;
            FolderPaths = new List<string>();
        }

        public Func<Dictionary<Guid, DateTimeOffset>> ReloadUpdateItems { get; protected set; }

        #region INIT

        public void VerifyThatUpdaterIsInit()
        {
            if (!_isUpdaterInit)
                throw new InvalidOperationException("Updater is not correct inititialized");
        }

        private bool _isUpdaterInit = false;

        internal void Init(List<string> folderPaths,
            Func<Dictionary<Guid, DateTimeOffset>> reloadForUpdateItemsMethod)
        {
            //TODO move instance information to better place
            InstanceInformation.RunningInstanceId = Guid.NewGuid();

            ReloadUpdateItems = reloadForUpdateItemsMethod;
            foreach (var folderPath in folderPaths)
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                CreateNewUpdateInfoIfNotExists(folderPath);
                LastReadedUpdateInfo = LoadUpdateInfo(folderPath);
            }
            ReloadUpdateItems.Invoke();
            _isUpdaterInit = true;
        }

        private void CreateNewUpdateInfoIfNotExists(string folderPath)
        {
            var expectedFilePath = Path.Combine(folderPath, UpdateFileName);
            if (!File.Exists(expectedFilePath))
            {
                var file = File.Create(expectedFilePath);
                file.Close();

                InitNewUpdateInfo(folderPath);
                this.LastReadedUpdateInfo.UpdatedItems = ReloadUpdateItems.Invoke();
                ContentChange(LastReadedUpdateInfo);
            }
        }

        internal void InitNewUpdateInfo(string folderPath)
        {
            LastReadedUpdateInfo = new ItemUpdateInfoDto();
            LastReadedUpdateInfo.LastModifiedDate = DateTimeOffset.UtcNow;
            LastReadedUpdateInfo.RunningInstanceId = InstanceInformation.RunningInstanceId;
            LastReadedUpdateInfo.IsUpdatedFromThisInstance = true;
            LastReadedUpdateInfo.UpdatedItems = new Dictionary<Guid, DateTimeOffset>();

            SaveUpdateInfo(folderPath, LastReadedUpdateInfo);

            _lastUpdateDate = LastReadedUpdateInfo.LastModifiedDate;
        }

        #endregion INIT

        public void StartExecution()
        {
            VerifyThatUpdaterIsInit();

            CancelExecution();
            _cts = new CancellationTokenSource();
            this.PollUpdateInfo(this._cts.Token);
        }

        public void CancelExecution()
        {
            this._cts.Cancel();
        }

        private DateTimeOffset _lastPollingExecution = DateTimeOffset.MinValue;

        /// <summary>
        /// "Infinite" loop that runs every N seconds. Good for checking for a heartbeat or updates.
        /// </summary>
        /// <param name="taskState">
        /// The cancellation token from our _cts field, passed in the StartNew call
        /// </param>
        private void PollUpdateInfo(object cancellationToken)
        {
            var token = (CancellationToken)cancellationToken;

            if (token.IsCancellationRequested)
                return;

            foreach (var folderPath in FolderPaths)
            {
                if (!string.IsNullOrEmpty(folderPath))
                {
                    if (Directory.Exists(folderPath))
                    {
                        HandleUpdateInfo(folderPath);
                    }
                }
            }
            _lastPollingExecution = DateTimeOffset.Now;
            countDown.Stop();
            countDown.Start();
        }

        private void CountDown_Elapsed(object sender, ElapsedEventArgs e)
        {
            countDown.Stop();

            this.PollUpdateInfo(_cts.Token);
        }

        private bool IsPollingServiceAlive()
        {
            if (_lastPollingExecution.AddMilliseconds(2 * DelayMs)
                .CompareTo(DateTimeOffset.Now) > 0)
            {
                return true;
            }
            return false;
        }

        internal ItemUpdateInfoDto LastReadedUpdateInfo { get; private set; }

        private DateTimeOffset _lastUpdateDate = DateTimeOffset.MinValue;

        internal bool HandleUpdateInfo(string folderPath)
        {
            VerifyThatUpdaterIsInit();

            CreateNewUpdateInfoIfNotExists(folderPath);
            var expectedFilePath = Path.Combine(folderPath, UpdateFileName);

            LastReadedUpdateInfo = LoadUpdateInfo(folderPath);

            if (!LastReadedUpdateInfo.RunningInstanceId.Equals(InstanceInformation.RunningInstanceId) &&
                LastReadedUpdateInfo.LastModifiedDate.CompareTo(_lastUpdateDate) > 0)
            {
                _lastUpdateDate = LastReadedUpdateInfo.LastModifiedDate;
                ContentChange(LastReadedUpdateInfo);
            }

            return true;
        }

        internal void RefereshAfterUpdate(string folderPath, Guid idUpdatedItem)
        {
            VerifyThatUpdaterIsInit();

            if (!IsPollingServiceAlive())
            {
                StartExecution();
                return;
            }
            LastReadedUpdateInfo = GetCurrentUpdateInformation(folderPath);

            if (LastReadedUpdateInfo.UpdatedItems.ContainsKey(idUpdatedItem))
            {
                LastReadedUpdateInfo.UpdatedItems[idUpdatedItem] = DateTimeOffset.UtcNow;
            }
            else
            {
                LastReadedUpdateInfo.UpdatedItems.Add(idUpdatedItem, DateTimeOffset.UtcNow);
            }

            SaveUpdateInfo(folderPath, LastReadedUpdateInfo);
            _lastUpdateDate = LastReadedUpdateInfo.LastModifiedDate;
        }

        internal void RefereshAfterDelete(string folderPath, Guid idDeletedItem)
        {
            VerifyThatUpdaterIsInit();

            if (!IsPollingServiceAlive())
            {
                StartExecution();
                return;
            }

            LastReadedUpdateInfo = GetCurrentUpdateInformation(folderPath);

            if (LastReadedUpdateInfo.UpdatedItems.ContainsKey(idDeletedItem))
            {
                LastReadedUpdateInfo.UpdatedItems.Remove(idDeletedItem);
            }

            SaveUpdateInfo(folderPath, LastReadedUpdateInfo);
            _lastUpdateDate = LastReadedUpdateInfo.LastModifiedDate;
        }

        internal void RefereshAfterDeleteAll(string folderPath)
        {
            VerifyThatUpdaterIsInit();

            if (!IsPollingServiceAlive())
            {
                StartExecution();
                return;
            }
            LastReadedUpdateInfo = GetCurrentUpdateInformation(folderPath);

            LastReadedUpdateInfo.UpdatedItems.Clear();

            SaveUpdateInfo(folderPath, LastReadedUpdateInfo);
            _lastUpdateDate = LastReadedUpdateInfo.LastModifiedDate;
        }

        private ItemUpdateInfoDto GetCurrentUpdateInformation(string folderPath)
        {
            CreateNewUpdateInfoIfNotExists(folderPath);
            var info = LoadUpdateInfo(folderPath);
            
            return info;
        }

        #region LOAD SAVE ITEMS

        private static bool SaveUpdateInfo(string folderPath, ItemUpdateInfoDto dto)
        {
            var expectedFilePath = Path.Combine(folderPath, UpdateFileName);
            dto.RunningInstanceId = InstanceInformation.RunningInstanceId;
            dto.LastModifiedDate = DateTimeOffset.Now;
            JsonStorageHandler.Store(Map(dto), expectedFilePath);

            return true;
        }

        private static ItemUpdateInfoDto LoadUpdateInfo(string folderPath)
        {
            var expectedFilePath = Path.Combine(folderPath, UpdateFileName);
            var loadedItem = JsonStorageHandler.Load<ItemUpdateInfo1X0>(expectedFilePath);

            return Map(loadedItem);
        }

        private static ItemUpdateInfo1X0 Map(ItemUpdateInfoDto dto)
        {
            ItemUpdateInfo1X0 storageItem = new ItemUpdateInfo1X0();
            storageItem.LastModifiedDate = dto.LastModifiedDate.ToString("O");
            storageItem.RunningInstanceId = dto.RunningInstanceId;
            storageItem.UpdatedItems = new Dictionary<Guid, string>();

            foreach (var item in dto.UpdatedItems)
            {
                if (!storageItem.UpdatedItems.ContainsKey(item.Key))
                {
                    storageItem.UpdatedItems.Add(item.Key, item.Value.ToString("O"));
                }
            }

            return storageItem;
        }

        private static ItemUpdateInfoDto Map(ItemUpdateInfo1X0 storageItem)
        {
            ItemUpdateInfoDto dto = new ItemUpdateInfoDto();
            dto.LastModifiedDate = DateTimeOffset.Parse(storageItem.LastModifiedDate);
            dto.RunningInstanceId = storageItem.RunningInstanceId;
            dto.IsUpdatedFromThisInstance = InstanceInformation.RunningInstanceId
                .Equals(storageItem.RunningInstanceId);

            dto.UpdatedItems = new Dictionary<Guid, DateTimeOffset>();
            foreach (var item in storageItem.UpdatedItems)
            {
                if (!dto.UpdatedItems.ContainsKey(item.Key))
                {
                    dto.UpdatedItems.Add(item.Key, DateTimeOffset.Parse(item.Value));
                }
            }

            return dto;
        }

        #endregion LOAD SAVE ITEMS
    }
}