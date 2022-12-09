using ScriptExecutorLib.Model.Execution;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TsSolution.WpfCommon;
using TsSolutions.Service;

namespace ScriptExecutorPrime.ViewModel
{
    internal class ScriptManagementViewModel : ViewModelBase
    {
        private readonly IExecutionItemManager _executionItemManager;

        public ScriptManagementViewModel(IExecutionItemManager executionItemManager)
        {
            this._executionItemManager = executionItemManager;
        }

        protected override async Task OnLoadingAfterInitializeAsync()
        {
        }

        public string _countTestScripts = "5";

        public String CountTestScripts
        {
            get { return _countTestScripts; }
            set
            {
                _countTestScripts = value;
                NotifyPropertyChanged();
            }
        }

        internal void CreateTestScripts()
        {
            CreateTest();
        }

        internal void CreateTest()
        {
            var numberItems = int.Parse(CountTestScripts);
            var items = CreateTestItems(numberItems);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            ExecutionHelper.RunSync(async () =>
            {
                foreach (var item in items)
                {
                    await _executionItemManager.Add(item);
                }
            });

            sw.Stop();
            Console.WriteLine($"{nameof(CreateTest)} : elapsed ms => {sw.ElapsedMilliseconds}");
        }

        internal void RunExecutionTemplate()
        {
        }

        private List<ExecutionItem> CreateTestItems(int numberItems)
        {
            List<ExecutionItem> testitems = new List<ExecutionItem>();

            for (int i = 0; i < numberItems; i++)
            {
                var item = ExecutionItemFactory.CreateExecutionItemDefault($"test{i}");
                testitems.Add(item);
            }

            return testitems;
        }

        internal async Task DeleteAllTestScripts()
        {
            await _executionItemManager.DeleteAll();
        }
    }
}