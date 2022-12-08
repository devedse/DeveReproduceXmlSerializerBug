using System;
using System.Threading;
using System.Threading.Tasks;

namespace DeveReproduceXmlSerializerBug.Threading
{
    public static class SemaphoreSlimExtensions
    {
        public static void Run(this SemaphoreSlim semaphore, Action action, CancellationToken cancellationToken = default)
        {
            semaphore.Wait(cancellationToken);

            try
            {
                action();
            }
            finally
            {
                semaphore.Release();
            }
        }

        public static async Task RunAsync(this SemaphoreSlim semaphore, Func<Task> action, CancellationToken cancellationToken = default)
        {
            await semaphore.WaitAsync(cancellationToken);

            try
            {
                await action();
            }
            finally
            {
                semaphore.Release();
            }
        }

        public static SemaphoreDisposer DisposableWait(this SemaphoreSlim semaphore, CancellationToken cancellationToken = default)
        {
            semaphore.Wait(cancellationToken);
            return new SemaphoreDisposer(semaphore);
        }

        public static async Task<SemaphoreDisposer> DisposableWaitAsync(this SemaphoreSlim semaphore, CancellationToken cancellationToken = default)
        {
            await semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
            return new SemaphoreDisposer(semaphore);
        }

        public struct SemaphoreDisposer : IDisposable
        {
            private readonly SemaphoreSlim _semaphore;

            public SemaphoreDisposer(SemaphoreSlim semaphore)
            {
                _semaphore = semaphore;
            }

            public void Dispose()
            {
                _semaphore.Release();
            }
        }
    }
}
