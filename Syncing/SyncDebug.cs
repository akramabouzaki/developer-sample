using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DeveloperSample.Syncing
{
    public class SyncDebug
    {
        private readonly object countLock = new object();

        public List<string> InitializeList(IEnumerable<string> items)
        {
            var bag = new ConcurrentBag<string>();
            Parallel.ForEach(items, async i =>
            {
                var taskRun = await Task.Run(() => i).ConfigureAwait(false);
                bag.Add(taskRun);
            });
            while (items.Count() > bag.Count) 
            { 
                Thread.Sleep(1); 
            }
            return bag.ToList();
        }

        public Dictionary<int, string> InitializeDictionary(Func<int, string> getItem)
        {
            var itemsToInitialize = Enumerable.Range(0, 100).ToList();
            var concurrentDictionary = new ConcurrentDictionary<int, string>();
            var count = -1;
            var threads = Enumerable.Range(0, 3)
                .Select(i => new Thread(() =>
                {
                    while (count < (itemsToInitialize.Count - 1))
                    {
                        lock (countLock)
                        {
                            if (Interlocked.Increment(ref count) < itemsToInitialize.Count)
                            {
                                concurrentDictionary.AddOrUpdate(itemsToInitialize[ct], getItem, (_, s) => s);
                            }
                        }
                    }
                }))
                .ToList();

            foreach (var thread in threads)
            {
                thread.Start();
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }

            return concurrentDictionary.ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}
