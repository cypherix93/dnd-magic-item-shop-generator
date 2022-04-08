using System.Runtime.Serialization;
using MagicItemShop.Web.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MagicItemShop.Web.DMPG.Models
{
    public class MagicItem
    {
        [JsonProperty("Items")]
        public string Item { get; set; }

        [JsonProperty("Sane Price")]
        public string SanePrice { get; set; }

        [JsonProperty("DMPG Price")]
        public string DMPGPrice { get; set; }

        [JsonProperty("MBJV Price")]
        public string MBJVPrice { get; set; }

        [JsonProperty("DMG Price")]
        public string DMGPrice { get; set; }

        [JsonProperty("XGE Price")]
        public string XGEPrice { get; set; }

        public MagicItemRarity Rarity { get; set; }
        public string Source { get; set; }
        public string Page { get; set; }

        public MagicItemType? Type { get; set; }

        public string Attunement { get; set; }
    }

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