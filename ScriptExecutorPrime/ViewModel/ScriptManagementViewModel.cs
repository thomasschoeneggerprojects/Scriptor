using ScriptExecutorLib.Model.Execution;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TsSolution.WpfCommon;
using TsSolutions.Service.PerformanceTest;
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

        internal void CreateTestScripts()
        {
            CreateTest();
        }

        internal void CreateTest()
        {
            var items = CreateTestItems();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            ExecutionHelper.RunSync(async () =>
            {
                foreach (var item in items)
                {
                    await _executionItemManager.Update(item);
                }
            });

            sw.Stop();
            Console.WriteLine($"{nameof(CreateTest)} : elapsed ms => {sw.ElapsedMilliseconds}");
        }

        internal void RunExecutionTemplate()
        {
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
    }
}