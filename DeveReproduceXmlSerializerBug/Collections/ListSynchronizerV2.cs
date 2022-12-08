using System.Collections.Generic;

namespace DeveReproduceXmlSerializerBug.Collections
{
    public static class ListSynchronizerV2
    {
        public static void SynchronizeLists<T>(IList<T> source, IList<T> destination) where T : class
        {
            for (int i = destination.Count - 1; i >= source.Count; i--)
            {
                destination.RemoveAt(i);
            }

            for (int i = 0; i < destination.Count; i++)
            {
                if (destination[i] != source[i])
                {
                    destination[i] = source[i];
                }
            }

            for (int i = destination.Count; i < source.Count; i++)
            {
                destination.Add(source[i]);
            }
        }
    }
}
