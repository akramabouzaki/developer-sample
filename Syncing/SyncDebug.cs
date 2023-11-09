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

        public List<string> InitializeList(IEnumerable<string> items)
        {
            var bag = new ConcurrentBag<string>();
            var p = Parallel.ForEach(items, async i =>
            {
                var r = await Task.Run(() => i).ConfigureAwait(false);
                bag.Add(r);
            });
            while (items.Count() > bag.Count) { Thread.Sleep(1); }
            var list = bag.ToList();
            return list;
        }

        public Dictionary<int, string> InitializeDictionary(Func<int, string> getItem)
        {
            var itemsToInitialize = Enumerable.Range(0, 100).ToList();
            var concurrentDictionary = new ConcurrentDictionary<int, string>();
            var ct = -1;
            var threads = Enumerable.Range(0, 3)
                .Select(i => new Thread(() =>
                {
                    while (ct < (itemsToInitialize.Count - 1))
                    {
                        Interlocked.Increment(ref ct);
                        if (ct < itemsToInitialize.Count)
                            concurrentDictionary.AddOrUpdate(itemsToInitialize[ct], getItem, (_, s) => s);
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
