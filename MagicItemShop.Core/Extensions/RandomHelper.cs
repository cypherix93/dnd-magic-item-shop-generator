namespace MagicItemShop.Core.Extensions
{
    public static class RandomHelper
    {
        private static readonly Random _random = new();

        public static bool RandomBool()
        {
            return new[] { true, false }.PickRandom();
        }

        public static int PickBetween(int min, int max)
        {
            return _random.Next(min, max + 1);
        }

        public static T PickRandom<T>(this IList<T> items)
        {
            return PickRandom(items, 1).First();
        }

        public static IList<T> PickRandom<T>(this IList<T> items, int count)
        {
            var result = new List<T>();

            while (result.Count < count)
            {
                var randomItem = items[_random.Next(items.Count)];

                if (!result.Contains(randomItem))
                {
                    result.Add(randomItem);
                }
            }

            return result;
        }
    }
}