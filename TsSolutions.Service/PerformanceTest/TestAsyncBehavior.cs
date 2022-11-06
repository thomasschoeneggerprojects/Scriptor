using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TsSolutions.Service.PerformanceTest
{
    internal class TestService : SimpleThreadServiceAsync
    {
        protected override void OnErrorOccured(ServiceErrorInformation errorInformation)
        {
            Trace.WriteLine($"{nameof(OnErrorOccured)} executed.");
        }

        public async Task TestTask(int number)
        {
            await LockAsync(async () =>
            {
                var currThreadId = Thread.CurrentThread.ManagedThreadId;
                Trace.WriteLine($"Task [{number}] with Thread id[{currThreadId}] is started.");
                Random random = new Random();
                var delayMs = random.Next(100, 300);
                await Task.Delay(random.Next(100, 300));

                Trace.WriteLine($"Task [{number}] with Thread id[{currThreadId}] is finished.Delay {delayMs}");
            });
        }
    }

    public class TestAsyncBehavior
    {
        private TestService _service = new TestService();

        public async Task RunTestMultipleThreadsAccessServiceUnderLockAsync()
        {
            Thread thread1 = new Thread(RunTestTasksLarge);
            Thread thread2 = new Thread(RunTestTasksMedium);
            Thread thread3 = new Thread(RunTestTasksSmall);
            thread1.Start();
            thread2.Start();
            thread3.Start();
        }

        private void RunTestTasksLarge()
        {
            RunTestTasks(0, 50, _service);
        }

        private void RunTestTasksMedium()
        {
            RunTestTasks(100, 25, _service);
        }

        private void RunTestTasksSmall()
        {
            RunTestTasks(200, 10, _service);
        }

        private async Task RunTestTasks(int offset, int numberOfTasks, TestService service)
        {
            List<Task> awaitableTasks = new List<Task>();
            for (int i = offset; i < offset + numberOfTasks; i++)
            {
                var task = service.TestTask(i);
                awaitableTasks.Add(task);
            }

            await Task.WhenAll(awaitableTasks);
        }

        public void TestRunSync()
        {
            Trace.WriteLine($"Before {nameof(ExecutionHelper.RunSync)}");

            ExecutionHelper.RunSync(async () =>
            {
                await RunTestTasks(300, 20, _service).ConfigureAwait(false);
            });

            Trace.WriteLine($"After {nameof(ExecutionHelper.RunSync)}");
        }
    }
}