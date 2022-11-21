using ScriptExecutorLib.Model.Execution;
using ScriptExecutorLib.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using TsSolution.WpfCommon;
using TsSolutions.Service;
using TsSolutions.Service.PerformanceTest;
using TsSolutions.WpfCommon.Controls;

namespace ScriptExecutorLib.ViewModel
{
    internal class ScriptExecutionViewModel : ViewModelBase
    {
        private readonly IExecutionItemManager _executionItemManager;
        private readonly IExecutionItemProcessor _executionItemProcessor;

        public ScriptExecutionViewModel(IExecutionItemManager executionItemManager,
            IExecutionItemProcessor executionItemProcessor)
        {
            this._executionItemManager = executionItemManager;
            this._executionItemProcessor = executionItemProcessor;

            InitViewModel();
        }

        protected override async Task OnLoadingAfterInitializeAsync()
        {
            var allItems = await _executionItemManager.GetAll();
            _FillItemList(allItems);
        }

        internal void AddNewExecutionItem()
        {
            var items = CreateTestItems();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            TestAsyncBehavior test = new TestAsyncBehavior();

            test.TestRunSync();
            ExecutionHelper.RunSync(async () =>
            {
                //foreach (var item in items)
                //{
                //    await _executionItemManager.Update(item);
                //}
                TestAsyncBehavior test = new TestAsyncBehavior();
                // await test.RunTestMultipleThreadsAccessServiceUnderLockAsync();
                test.TestRunSync();
            });

            sw.Stop();
            Console.WriteLine($"{nameof(AddNewExecutionItem)} : elapsed ms => {sw.ElapsedMilliseconds}");
        }

        private List<ExecutionItem> CreateTestItems()
        {
            List<ExecutionItem> testitems = new List<ExecutionItem>();

            for (int i = 0; i < 1000; i++)
            {
                var item = ExecutionItemFactory.CreateExecutionItemDefault($"test{i}");
                testitems.Add(item);
            }

            return testitems;
        }

        #region Execution items list

        private void _FillItemList(List<ExecutionItem> allItems)
        {
            List<UserControl> items = new List<UserControl>();
            RunOnUIThread(() =>
            {
                foreach (var item in allItems)
                {
                    var cntrl = new ScriptControl(item);

                    items.Add(cntrl);
                }

                SetItems(items);
            });
        }

        private List<UserControl> _executionItems;

        public List<UserControl> ExecutionItems
        {
            get
            {
                if (_executionItems == null)
                {
                    var executionItems = new List<UserControl>();
                    _executionItems = executionItems;
                }
                return _executionItems;
            }
            set
            {
                _executionItems = value;
                NotifyPropertyChanged();
            }
        }

        private UserControl _selectedExecutionItem;

        public UserControl SelectedExecutionItem
        {
            get
            {
                return _selectedExecutionItem;
            }
            set
            {
                _selectedExecutionItem = value;
                SetSelectedItemContent(_selectedExecutionItem);
                NotifyPropertyChanged();
            }
        }

        private void SetSelectedItemContent(UserControl _selectedExecutionItem)
        {
            if (_selectedExecutionItem is ScriptControl scriptControl)
            {
                ScriptOverView scriptOverView = new ScriptOverView(scriptControl.Item);
                SelectedScriptOverView = scriptOverView;
            }
        }

        private UserControl _selectedScriptOverView;

        public UserControl SelectedScriptOverView
        {
            get
            {
                return _selectedScriptOverView;
            }
            set
            {
                _selectedScriptOverView = value;
                NotifyPropertyChanged();
            }
        }

        internal void SetItems(List<UserControl> items)
        {
            ExecutionItems = items;

            if (ExecutionItems?.Count > 0)
            {
                SelectedExecutionItem = ExecutionItems.ElementAt(0);
            }
        }

        internal void DeleteSelectedExecutionItem()
        {
            throw new NotImplementedException();
        }

        #endregion Execution items list
    }
}