using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TsSolutions.Storage.FileStorage;

namespace TsSolutions.Storage
{
    public abstract class RepositoryBase<T, Dto>
    {
        public List<ItemOverviewInfo> OverviewItems { get; protected set; } = new List<ItemOverviewInfo>();

        public Dictionary<Guid, Dto> AllItemsDictionary { get; protected set; } = new Dictionary<Guid, Dto>();

        public List<Dto> AllItems { get; protected set; } = new List<Dto>();

        public ItemUpdater<bool> Updater { get; private set; }

        private object lockSync = new object();

        protected RepositoryBase()
        {
        }

        #region Init Updater

        private bool _isUpdaterInit = false;

        private async Task InitUpdaterFirst()
        {
            if (_isUpdaterInit)
            {
                return;
            }

            Updater = new ItemUpdater<bool>();
            FolderPaths = await GetFolderPaths();

            Updater.ContentChange += async (uInf) => { await ContentFolderChanged(uInf); };
            Updater.Init(FolderPaths, () =>
            {
                return LoadUpdateItems();
            });

            _isUpdaterInit = true;
        }

        private Dictionary<Guid, DateTimeOffset> LoadUpdateItems()
        {
            var updatedItems = new Dictionary<Guid, DateTimeOffset>();

            List<string> fileNames = GetAllRelevantFilenames(FolderPaths);

            if (fileNames != null)
            {
                foreach (var fileName in fileNames)
                {
                    if (VerifyFileType(fileName))
                    {
                        var fileInfo = new FileInfo(fileName);
                        var guid = GetGuidFromFileName(fileInfo.Name);

                        var item = LoadItem(fileName);
                        SetItemInDictionary(guid, item);
                        updatedItems.Add(guid, DateTimeOffset.Now);
                    }
                }
            }
            return updatedItems;
        }

        #endregion Init Updater

        private async Task ContentFolderChanged(ItemUpdateInfoDto updateInfo)
        {
            try
            {
                await ReloadItemsFromRepository();
            }
            catch (Exception ex)
            {
                // Provide logging interface
            }
        }

        public List<string> FolderPaths
        {
            get { return Updater.FolderPaths; }
            set { Updater.FolderPaths = value; }
        }

        public Func<string, bool> VerifyFileType { get; protected set; }

        public Dto LoadItem(string filePath)
        {
            var item = JsonStorageHandler.Load<T>(filePath);
            var dto = Map(item);
            return dto;
        }

        public async Task SaveItem(Dto item, Guid itemId, string filePath)
        {
            await InitUpdaterFirst().ConfigureAwait(false);
            JsonStorageHandler.Store<T>(Map(item), filePath);

            SetItemInDictionary(itemId, item);

            var folderPath = Path.GetDirectoryName(filePath);
            await HandleUpdateInStorage(itemId, folderPath);
        }

        public async Task DeleteItem(Guid itemId, string filePath)
        {
            await InitUpdaterFirst().ConfigureAwait(false);
            File.Delete(filePath);
            RemoveItemFromDictionary(itemId);

            var folderPath = Path.GetDirectoryName(filePath);
            await HandleDeleteInStorage(itemId, folderPath);
        }

        public async Task DeleteAllItem(string folderPath)
        {
            await InitUpdaterFirst().ConfigureAwait(false);
            foreach (var item in AllItems)
            {
                var filePath = CreateFilePath(item);

                File.Delete(filePath);
            }
            ClearDictionary();

            await HandleDeleteAllInStorage(folderPath);
        }

        public async Task HandleDeleteAllInStorage(string folderPath)
        {
            await InitUpdaterFirst().ConfigureAwait(false);

            Updater.RefereshAfterDeleteAll(folderPath);
            await ReloadItemsFromRepository();
        }

        public async Task HandleDeleteInStorage(Guid itemId, string folderPath)
        {
            await InitUpdaterFirst().ConfigureAwait(false);

            Updater.RefereshAfterDelete(folderPath, itemId);
            await ReloadItemsFromRepository();
        }

        public async Task HandleUpdateInStorage(Guid itemId, string folderPath)
        {
            await InitUpdaterFirst().ConfigureAwait(false);

            Updater.RefereshAfterUpdate(folderPath, itemId);
            await ReloadItemsFromRepository();
        }

        public async Task RefreshFromStorage()
        {
            await ReloadItemsFromRepository();
        }

        #region Update

        private void RaiseStorageHasChanged()
        {
            StorageChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler StorageChanged;

        private async Task ReloadItemsFromRepository()
        {
            await InitUpdaterFirst().ConfigureAwait(false);

            try

            {
                lock (lockSync)
                {
                    List<string> fileNames = GetAllRelevantFilenames(FolderPaths);

                    if (fileNames != null)
                    {
                        foreach (var fileName in fileNames)
                        {
                            if (VerifyFileType(fileName))
                            {
                                var fileInfo = new FileInfo(fileName);

                                var guid = GetGuidFromFileName(fileInfo.Name);

                                if (IsItemToReload(Updater.LastReadedUpdateInfo,
                                    guid, AllItemsDictionary))
                                {
                                    var item = LoadItem(fileName);
                                    SetItemInDictionary(guid, item);
                                }
                            }
                        }

                        AllItems = new List<Dto>(AllItemsDictionary.Values);
                        CreateOverviewItems(AllItems);
                    }
                }

                RaiseStorageHasChanged();
            }
            catch (Exception ex)
            {
                // Provide logging interface
            }
        }

        private void SetItemInDictionary(Guid guid, Dto item)
        {
            if (AllItemsDictionary.ContainsKey(guid))
            {
                AllItemsDictionary[guid] = item;
            }
            else
            {
                AllItemsDictionary.Add(guid, item);
            }
        }

        private void RemoveItemFromDictionary(Guid guid)
        {
            if (AllItemsDictionary.ContainsKey(guid))
            {
                AllItemsDictionary.Remove(guid);
            }
        }

        private void ClearDictionary()
        {
            AllItemsDictionary.Clear();
        }

        private static List<string> GetAllRelevantFilenames(List<string> folderPaths)
        {
            List<string> fileNames = new List<string>();
            foreach (var folderPath in folderPaths)
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                fileNames.AddRange(Directory.GetFiles(folderPath));
            }

            return fileNames;
        }

        private bool IsItemToReload(ItemUpdateInfoDto updateInfo,
            Guid guid, Dictionary<Guid, Dto> allItemsDictionary)
        {
            if (updateInfo.IsUpdatedFromThisInstance)
                return false;

            if (!allItemsDictionary.ContainsKey(guid))
            {
                return true;
            }

            if (updateInfo.UpdatedItems.ContainsKey(guid))
            {
                // update if changed item
                return updateInfo.UpdatedItems[guid]
                    .CompareTo(updateInfo.LastModifiedDate) > 0;
            }
            return false;
        }

        private Guid GetGuidFromFileName(string name)
        {
            var parts = name.Split('.');

            return Guid.Parse(parts[parts.Length - 2]);
        }

        #endregion Update

        #region CRUD interface

        public abstract Task<Dto> Load(string filePath);

        public abstract Task Save(Dto item, string filePath);

        public abstract void CreateOverviewItems(List<Dto> items);

        public abstract Task<List<string>> GetFolderPaths();

        public abstract string CreateFilePath(Dto item);

        public abstract Task Delete(Dto item);

        public abstract Task DeleteAll();

        #endregion CRUD interface

        #region Mapping

        public abstract Dto Map(T item);

        public abstract T Map(Dto dto);

        #endregion Mapping
    }
}