using MagicItemShop.Core.App.Sources.DMPG.Models;

namespace MagicItemShop.Core.App.MagicItemShop.Models
{
    public class MagicItemShopGenerateRequest
    {
        public MagicItemRarity[] Rarities { get; set; } =
        {
            MagicItemRarity.Common,
            MagicItemRarity.Uncommon,
            MagicItemRarity.Rare,
            MagicItemRarity.VeryRare,
            MagicItemRarity.Legendary,
            MagicItemRarity.Artifact
        };

        public MagicItemType[] ItemTypes { get; set; } = Array.Empty<MagicItemType>();

        public MagicItemShopRarityQuantityMultipliers RarityQuantityMultipliers { get; set; } = new()
        {
            { MagicItemRarity.Common, 48 },
            { MagicItemRarity.Uncommon, 24 },
            { MagicItemRarity.Rare, 16 },
            { MagicItemRarity.VeryRare, 8 },
            { MagicItemRarity.Legendary, 2 },
            { MagicItemRarity.Artifact, 1 },
            { MagicItemRarity.Varies, 8 }
        };

        public SourceBook[] Sources { get; set; } =
        {
            SourceBook.DMG,
            SourceBook.OA,
            SourceBook.VGM,
            SourceBook.XGE,
            SourceBook.EBR,
            SourceBook.WGE,
            SourceBook.TCE
        };

        public decimal ShopDiscount { get; set; }

        public decimal ShopItemsQuantityMultiplier { get; set; }
    }

    public class MagicItemShopRarityQuantityMultipliers : Dictionary<MagicItemRarity, decimal>
    {
        public new decimal this[MagicItemRarity key]
        {
            get => this.GetValueOrDefault(key);
            set => base[key] = value;
        }
    }
}