using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TsSolutions.Service
{
    public static class ExecutionHelper
    {
        public static T RunSync<T>(Func<Task<T>> worker)
        {
            T result = default(T);
            var autoResetEvent = new AutoResetEvent(false);
            Exception exception = null;
            bool errorOccured = false;
            _ = Task.Run(async () =>
            {
                try
                {
                    result = await worker();
                }
                catch (Exception ex)
                {
                    exception = ex;
                    errorOccured = true;
                }
                finally
                {
                    autoResetEvent.Set();
                }
            });

            autoResetEvent.WaitOne();
            if (errorOccured)
            {
                throw exception;
            }

            return result;
        }

        public static void RunSync(Func<Task> worker)
        {
            var autoResetEvent = new AutoResetEvent(false);
            Exception exception = null;
            bool errorOccured = false;
            _ = Task.Run(async () =>
            {
                try
                {
                    await worker();
                }
                catch (Exception ex)
                {
                    exception = ex;
                    errorOccured = true;
                }
                finally
                {
                    autoResetEvent.Set();
                }
            });

            autoResetEvent.WaitOne();

            if (errorOccured)
            {
                throw exception;
            }

            return;
        }
    }
}