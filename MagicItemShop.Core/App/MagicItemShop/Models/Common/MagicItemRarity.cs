using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MagicItemShop.Core.App.MagicItemShop.Models.Common
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MagicItemRarity
    {
        Common,
        Uncommon,
        Rare,

        [EnumMember(Value = "Very Rare")]
        VeryRare,
        Legendary,
        Artifact,
        Varies
    }
}