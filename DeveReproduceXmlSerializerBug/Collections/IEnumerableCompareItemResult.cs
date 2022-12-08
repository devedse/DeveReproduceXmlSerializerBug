namespace DeveReproduceXmlSerializerBug.Collections
{
    public class IEnumerableCompareItemResult<T>
    {
        public T Old { get; set; }
        public T New { get; set; }

        public IEnumerableCompareItemResult(T oldItem, T newItem)
        {
            Old = oldItem;
            New = newItem;
        }
    }
}
