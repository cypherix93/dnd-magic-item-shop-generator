using System.Runtime.Serialization;
using MagicItemShop.Web.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MagicItemShop.Web.DMPG.Models
{
    public class MagicItem
    {
        [JsonProperty("Item")]
        public string ItemName { get; set; }

        public string ItemUrl { get; set; }

        public string SanePrice { get; set; }
        public string DMPGPrice { get; set; }
        public string MBJVPrice { get; set; }
        public string DMGPrice { get; set; }
        public string XGEPrice { get; set; }

        public MagicItemRarity Rarity { get; set; }
        public SourceBook Source { get; set; }
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

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SourceBook
    {
        [EnumMember(Value = "Acquistions Incorporated")]
        AI,

        [EnumMember(Value = "Basic Rules")]
        BR,

        [EnumMember(Value = "Dungeon Master's Guide")]
        DMG,

        [EnumMember(Value = "Explorer's Guide to Wildemount")]
        EGW,

        [EnumMember(Value = "Eberron: Rising From The Last War")]
        EBR,

        [EnumMember(Value = "Fizban's Treasure of Dragons")]
        FTD,

        [EnumMember(Value = "Guildmasters' Guide to Ravnica")]
        GGR,

        [EnumMember(Value = "Icewind Dale: Rime of the Frostmaiden")]
        ID,

        [EnumMember(Value = "Mystic Odysseys of Theros")]
        MOoT,

        [EnumMember(Value = "Mordenkainen's Tome of Foes")]
        MTF,

        [EnumMember(Value = "Strixhaven: A Curriculum of Chaos")]
        STRX,

        [EnumMember(Value = "Tomb of Annihilation")]
        TA,

        [EnumMember(Value = "Tasha's Cauldron of Everything")]
        TCE,

        [EnumMember(Value = "Tales from the Yawning Portal")]
        TYP,

        [EnumMember(Value = "Unearthed Arcana")]
        UA,

        [EnumMember(Value = "Volo's Guide to Monsters")]
        VGM,

        [EnumMember(Value = "Waterdeep: Dragon Heist")]
        WDH,

        [EnumMember(Value = "Wayfarer's Guide to Eberron")]
        WGE,

        [EnumMember(Value = "Xanathar's Guide to Everything")]
        XGE,

        [EnumMember(Value = "Baldur's Gate: Descent into Avernus")]
        BGA,

        [EnumMember(Value = "Critical Role: Call of the Netherdeep")]
        CotN,

        [EnumMember(Value = "Curse of Strahd")]
        CS,

        [EnumMember(Value = "Divine Contention")]
        DC,

        [EnumMember(Value = "Waterdeep: Dungeon of the Mad Mage")]
        DMM,

        [EnumMember(Value = "Ghosts of Saltmarsh")]
        GoS,

        [EnumMember(Value = "Hoard of the Dragon Queen")]
        HDQ,

        [EnumMember(Value = "Hunt for the Thessalhydra")]
        HT,

        [EnumMember(Value = "Lost Laboratory of Kwalish")]
        LLK,

        [EnumMember(Value = "Lost Mine of Phandelver")]
        LMP,

        [EnumMember(Value = "Out of the Abyss")]
        OA,

        [EnumMember(Value = "Princes of the Apocalypse")]
        PA,

        [EnumMember(Value = "Rise of Tiamat")]
        RT,

        [EnumMember(Value = "Sleeping Dragon’s Wake")]
        SDW,

        [EnumMember(Value = "Storm King's Thunder")]
        SKT,

        [EnumMember(Value = "Tomb of Horrors")]
        TH,

        [EnumMember(Value = "The Wild Beyond the Witchlight")]
        TWBTW,
    }
}