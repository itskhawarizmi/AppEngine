using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AppEngine
{
    /// <summary>
    /// Adds to ability to safely await on task to be complete that 
    /// need limited access.
    /// </summary>
    public static class AsyncEngine
    {
        #region Members

        /// <summary>
        /// A semaphore to lock semaphore list.
        /// </summary>
        private static SemaphoreSlim SelfLock = new SemaphoreSlim(1, 1);

        /// <summary>
        /// A list of all semaphore locks (just only per key).
        /// </summary>
        private static Dictionary<string, SemaphoreSlim> SemaphoreList = new Dictionary<string, SemaphoreSlim>();

        #endregion

        /// <summary>
        /// Awaits for any outstanding tasks to complete that are accessing the same key then runs the given task
        /// </summary>
        /// <param name="key">The key to await</param>
        /// <param name="task">The task to perform inside the semaphore lock</param>
        /// <param name="maxAccessCount">If this is the first call, sets the maximum number of task that can access this task before it waiting</param>
        /// <returns></returns>
        public static async Task AwaitAsync(string key, Func<Task> task, int maxAccessCount = 1)
        {
            await SelfLock.WaitAsync();

            try
            {
                if (!SemaphoreList.ContainsKey(key))
                    SemaphoreList.Add(key, new SemaphoreSlim(maxAccessCount, maxAccessCount));
            }
            catch (Exception Ex)
            {
                // TODO ADD LOG
            }
            finally
            {
                SelfLock.Release();
            }

            var semaphore = SemaphoreList[key];

            await semaphore.WaitAsync();

            try
            {
               await task();
            }
            catch (Exception Ex)
            {
                // TODO ADD LOG
            }
            finally
            {
                semaphore.Release();
            }
        }

        /// Awaits for any outstanding tasks to complete that are accessing the same key then runs the given task
        /// </summary>
        /// <param name="key">The key to await</param>
        /// <param name="task">The task to perform inside the semaphore lock</param>
        /// <param name="maxAccessCount">If this is the first call, sets the maximum number of task that can access this task before it waiting</param>
        /// <returns></returns>
        public static async Task<T> AwaitResultAsync<T>(string key, Func<Task<T>> task, int maxAccessCount = 1)
        {
            await SelfLock.WaitAsync();

            try
            {
                if (!SemaphoreList.ContainsKey(key))
                    SemaphoreList.Add(key, new SemaphoreSlim(maxAccessCount, maxAccessCount));
            }
            catch(Exception Ex)
            {
                // TODO ADD LOG
            }
            finally
            {
                SelfLock.Release();
            }

            var semaphore = SemaphoreList[key];

            await semaphore.WaitAsync();

            try
            {
               return await task();
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
