using System.Runtime.Serialization;
using MagicItemShop.Core.Json;
using Newtonsoft.Json;

namespace MagicItemShop.Core.App.MagicItemShop.Models.Common
{
    [JsonConverter(typeof(MagicItemTypeConverter))]
    public enum MagicItemType
    {
        Ammunition,
        Armor,

        [EnumMember(Value = "Potion / Oil")]
        PotionOil,
        Ring,
        Rod,
        Shield,

        [EnumMember(Value = "Spell Gem")]
        SpellGem,

        [EnumMember(Value = "Spell Scroll")]
        SpellScroll,

        Staff,
        Wand,
        Weapon,

        [EnumMember(Value = "Wondrous Item")]
        WondrousItem
    }
}