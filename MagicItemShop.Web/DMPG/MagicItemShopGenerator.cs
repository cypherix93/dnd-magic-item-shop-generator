using MagicItemShop.Web.DMPG.Models;
using MagicItemShop.Web.Extensions;
using Newtonsoft.Json;

namespace MagicItemShop.Web.DMPG
{
    public static class MagicItemShopGenerator
    {
        private static readonly ShopNamesData _shopNamesData;

        private static readonly SortedSet<SourceBook> _availableSourceBooks = new()
        {
            SourceBook.DMG,
            SourceBook.OA,
            SourceBook.VGM,
            SourceBook.XGE,
            SourceBook.EBR,
            SourceBook.WGE,
            SourceBook.TCE
        };

        private static readonly Dictionary<MagicItemRarity, decimal> _rarityQuantityMultiplier = new()
        {
            { MagicItemRarity.Common, 48 },
            { MagicItemRarity.Uncommon, 24 },
            { MagicItemRarity.Rare, 16 },
            { MagicItemRarity.VeryRare, 8 },
            { MagicItemRarity.Legendary, 2 },
            { MagicItemRarity.Artifact, 1 },
            { MagicItemRarity.Varies, 8 }
        };

        private static readonly Dictionary<MagicItemRarity, (int min, int max)> _rarityBaseQuantities = _rarityQuantityMultiplier
            .ToDictionary(x => x.Key, x => (min: (int)(x.Value / 2), max: (int)x.Value));

        static MagicItemShopGenerator()
        {
            _shopNamesData = JsonConvert.DeserializeObject<ShopNamesData>(
                File.ReadAllText("Resources/shopnames.json")
            );
        }

        public static Models.MagicItemShop GenerateMagicItemShop()
        {
            var availableItems = MagicItemDatabase.Items
                .Select(x => new MagicItemShopItem(x))
                .Where(x => _availableSourceBooks.Contains(x.Source)) // filter to only available source books
                .Where(x => x.Rarity != MagicItemRarity.Varies) // exclude varying rarity // TODO: need to include later
                .Where(x => !(x.Rarity < MagicItemRarity.VeryRare && x.Price == 0)) // exclude all 0 price items below VeryRare
                .ToList();

            var groupedByRarity = availableItems
                .GroupBy(x => x.Rarity)
                .ToDictionary(x => x.Key, x => x.ToList());

            var inventory = groupedByRarity.Keys
                .SelectMany(key =>
                    groupedByRarity[key]
                        .PickRandom(
                            RandomHelper.PickBetween(_rarityBaseQuantities[key].min, _rarityBaseQuantities[key].max)
                        )
                )
                .OrderBy(item => item.Rarity)
                .ThenBy(item => item.Name)
                .ToList();

            return new(
                GenerateShopName(),
                inventory
            );
        }

        public static string GenerateShopName()
        {
            var shouldUseFullName = RandomHelper.RandomBool();
            var shouldUseThe = RandomHelper.RandomBool();

            if (shouldUseFullName)
            {
                return _shopNamesData.FullNames.PickRandom();
            }

            return $"{(shouldUseThe ? "The " : "")}{_shopNamesData.Prefixes.PickRandom()} {_shopNamesData.Suffixes.PickRandom()}";
        }

        public class ShopNamesData
        {
            public List<string> FullNames { get; set; }
            public List<string> Prefixes { get; set; }
            public List<string> Suffixes { get; set; }
        }
    }
}