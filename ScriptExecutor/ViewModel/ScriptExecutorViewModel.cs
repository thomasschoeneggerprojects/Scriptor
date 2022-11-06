using ScriptExecutorLib.Model.Execution;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TsSolution.WpfCommon;
using TsSolutions.Service;
using TsSolutions.Service.PerformanceTest;

namespace ScriptExecutorLib.ViewModel
{
    internal class ScriptExecutorViewModel : ViewModelBase
    {
        private readonly IExecutionItemManager _executionItemManager;

        public ScriptExecutorViewModel(IExecutionItemManager executionItemManager)
        {
            this._executionItemManager = executionItemManager;
        }

        internal void CreateTest()
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
            Console.WriteLine($"{nameof(CreateTest)} : elapsed ms => {sw.ElapsedMilliseconds}");
        }

        internal void DeleteTest()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            try
            {
                ExecutionHelper.RunSync(async () =>
                {
                    await _executionItemManager.DeleteAll();
                });
            }
            catch (Exception)
            {
                throw;
            }

            sw.Stop();
            Console.WriteLine($"{nameof(CreateTest)} : elapsed ms => {sw.ElapsedMilliseconds}");
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