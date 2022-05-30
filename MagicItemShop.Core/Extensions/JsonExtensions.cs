using Newtonsoft.Json;

namespace MagicItemShop.Core.Extensions
{
    public static class JsonExtensions
    {
        public static T JsonClone<T>(this T source)
        {
            return JsonConvert.DeserializeObject<T>(
                JsonConvert.SerializeObject(source)
            );
        }
    }
}