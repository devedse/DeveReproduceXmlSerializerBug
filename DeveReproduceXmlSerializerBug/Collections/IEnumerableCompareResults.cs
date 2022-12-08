using System.Collections.Generic;

namespace DeveReproduceXmlSerializerBug.Collections
{
    public class IEnumerableCompareResults<T>
    {
        public List<T> Added { get; }
        public List<T> Removed { get; }
        public List<IEnumerableCompareItemResult<T>> Updated { get; }

        public IEnumerableCompareResults(List<T> added, List<T> removed, List<IEnumerableCompareItemResult<T>> updated)
        {
            Added = added;
            Removed = removed;
            Updated = updated;
        }
    }
}
