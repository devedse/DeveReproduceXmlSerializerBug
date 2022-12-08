using System.Collections.Generic;
using System.Linq;

namespace DeveReproduceXmlSerializerBug.Collections
{
    public static class ListSynchronizer
    {
        public static void SynchronizeLists<T>(IList<T> source, IList<T> dest) where T : class
        {
            var sourceDist = source.Distinct().ToList();
            var destDist = dest.Distinct().ToList();

            var toRemoveCompletely = destDist.Except(sourceDist);

            foreach (var item in toRemoveCompletely)
            {
                RemoveAll(dest, item);
            }

            for (int i = 0; i < sourceDist.Count; i++)
            {
                var sourceItem = sourceDist[i];

                var countInSource = source.Count(t => t == sourceItem);
                var countInDest = dest.Count(t => t == sourceItem);

                while (countInDest > countInSource)
                {
                    var pos = dest.IndexOf(sourceItem);
                    dest.RemoveAt(pos);
                    countInDest--;
                }

                while (countInDest < countInSource)
                {
                    dest.Add(sourceItem);
                    countInDest++;
                }
            }

            //var dictCount = new Dictionary<T, int>();

            for (int i = 0; i < dest.Count; i++)
            {
                var sourceItem = source[i];
                var destItem = dest[i];

                if (sourceItem != destItem)
                {
                    var posToSwapFrom = FindNext(dest, sourceItem, i);

                    Swap(dest, posToSwapFrom, i);
                }
            }
        }

        public static void RemoveAll<T>(IList<T> list, T item) where T : class
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i] == item)
                {
                    list.RemoveAt(i);
                }
            }
        }

        //public static int GetFromDict<T>(Dictionary<T, int> dict, T item)
        //{
        //    if (dict.TryGetValue(item, out var value))
        //    {
        //        return value;
        //    }
        //    else
        //    {
        //        dict[item] = 0;
        //        return 0;
        //    }
        //}

        public static int FindNext<T>(IList<T> list, T item, int start) where T : class
        {
            for (int i = start; i < list.Count; i++)
            {
                var found = list[i];
                if (found == item)
                {
                    return i;
                }
            }
            return -1;
        }

        public static void Swap<T>(IList<T> list, int source, int dest)
        {
            var temp = list[dest];
            list[dest] = list[source];
            list[source] = temp;
        }
    }
}
