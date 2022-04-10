using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MagicItemShop.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string ToEnumMemberName(this Enum e)
        {
            return JToken.Parse(JsonConvert.SerializeObject(e)).ToString();
        }
    }
}