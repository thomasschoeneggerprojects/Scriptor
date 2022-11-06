using System;
using System.Threading;
using System.Threading.Tasks;

namespace TsSolutions.Service
{
    public abstract class SimpleThreadServiceAsync
    {
        protected abstract void OnErrorOccured(ServiceErrorInformation errorInformation);

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public async Task<T> LockAsync<T>(Func<Task<T>> worker)
        {
            await _semaphore.WaitAsync();
            bool isReleased = false;
            try
            {
                return await worker();
            }
            catch (Exception ex)
            {
                _semaphore.Release();
                isReleased = true;
                OnErrorOccured(ServiceErrorInformation.Create(
                    ex, $"Error execution in class {nameof(SimpleThreadServiceAsync)}", nameof(LockAsync)));
            }
            finally
            {
                if (!isReleased)
                    _semaphore.Release();
            }

            throw new InvalidOperationException($"Cannot run {nameof(SimpleThreadServiceAsync)}{nameof(LockAsync)}");
        }

        public async Task LockAsync(Func<Task> worker)
        {
            await _semaphore.WaitAsync();
            bool isReleased = false;
            try
            {
                await worker();
                return;
            }
            catch (Exception ex)
            {
                _semaphore.Release();
                isReleased = true;
                OnErrorOccured(ServiceErrorInformation.Create(
                    ex, $"Error execution in class {nameof(SimpleThreadServiceAsync)}", nameof(LockAsync)));
            }
            finally
            {
                if (!isReleased)
                    _semaphore.Release();
            }

            throw new InvalidOperationException($"Cannot run {nameof(SimpleThreadServiceAsync)}{nameof(LockAsync)}");
        }

        public T Lock<T>(Func<T> worker)
        {
            _semaphore.Wait();
            bool isReleased = false;
            try
            {
                return worker();
            }
            catch (Exception ex)
            {
                _semaphore.Release();
                isReleased = true;
                OnErrorOccured(ServiceErrorInformation.Create(
                    ex, $"Error execution in class {nameof(SimpleThreadServiceAsync)}", nameof(Lock)));
            }
            finally
            {
                if (!isReleased)
                    _semaphore.Release();
            }
            throw new InvalidOperationException($"Cannot run {nameof(SimpleThreadServiceAsync)}{nameof(Lock)}");
        }
    }
}