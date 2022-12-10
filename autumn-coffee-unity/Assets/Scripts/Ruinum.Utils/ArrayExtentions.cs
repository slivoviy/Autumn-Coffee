namespace Ruinum.Utils
{
    public static class ArrayExtentions
    {
        public static T AddElement<T>(this T[] array, T item)
        {
            int arrayLength = array.Length;
            
            array = new T[arrayLength + 1];
            array[arrayLength + 1] = item;
            
            return item;
        }

        public static void RemoveElement<T>(this T[] array, T item)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].GetHashCode() != item.GetHashCode()) continue;
                array[i] = default;
            }
        }

        public static T GetFirstElement<T>(this T[] array)
        {
            return array[0];
        }

        public static T GetElement<T>(this T[] array, T item)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].GetHashCode() != item.GetHashCode()) continue;
                return array[i];
            }

            return default;
        }

        public static T GetFirstNotNullElement<T>(this T[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null) return array[i];
            }

            return default;
        }
    }
}
