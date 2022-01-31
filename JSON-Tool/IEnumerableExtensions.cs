using System.Collections;

namespace JSON_Tool
{
    public static class IEnumerableExtensions
    {
        public static int Count(this IEnumerable collection)
        {
            int count = 0;

            foreach (var item in collection)
            {
                count++;
            }

            return count;
        }
    }
}
