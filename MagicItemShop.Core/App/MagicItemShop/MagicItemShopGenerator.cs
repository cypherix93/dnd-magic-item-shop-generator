using MagicItemShop.Core.App.MagicItemShop.Models;
using MagicItemShop.Core.App.Sources.DMPG;
using MagicItemShop.Core.App.Sources.DMPG.Models;
using MagicItemShop.Core.Extensions;

namespace MagicItemShop.Core.App.MagicItemShop
{
    public static class MagicItemShopGenerator
    {
        public static IEnumerable<MagicItemShopItem> GetAvailableItems()
        {
            return MagicItemDatabase.Items
                .Select(x => new MagicItemShopItem(x)); // filter to only available source books
        }

        public static Models.MagicItemShop GenerateMagicItemShop(MagicItemShopGenerateRequest request = null)
        {
            request ??= new();

            var filteredItems = GetAvailableItems();

            // item sources filter
            if (request.Sources.Any())
            {
                filteredItems = filteredItems
                    .Where(x => request.Sources.Contains(x.Source));
            }

            // item rarities filter
            if (request.Rarities.Any())
            {
                filteredItems = filteredItems
                    .Where(x => request.Rarities.Contains(x.Rarity));
            }

            // item types filter
            if (request.ItemTypes.Any())
            {
                filteredItems = filteredItems
                    .Where(x => request.ItemTypes.Contains(x.Type));
            }

            var availableItems = filteredItems
                .Where(x => !(x.Rarity < MagicItemRarity.VeryRare && x.Price == 0)) // exclude all 0 price items below VeryRare
                .ToList();

            var groupedByRarity = availableItems
                .GroupBy(x => x.Rarity)
                .ToDictionary(x => x.Key, x => x.ToList());

            var rarityQuantityRanges = GetRarityQuantityRanges(request.RarityQuantityMultipliers);
            var shopQuantityMultiplier = 1 + request.ShopItemsQuantityMultiplier;

            var inventory = groupedByRarity.Keys
                .SelectMany(key =>
                    groupedByRarity[key]
                        .PickRandom(
                            (int)Math.Round(RandomHelper.PickBetween(rarityQuantityRanges[key].min, rarityQuantityRanges[key].max) * shopQuantityMultiplier)
                        )
                )
                .OrderBy(item => item.Rarity)
                .ThenBy(item => item.Name)
                .ToList();

            var magicItemShop = new Models.MagicItemShop(
                inventory, MagicItemShopNames.GenerateShopName()
            );

            // calculate discounts
            if (request.ShopDiscount != 0)
            {
                magicItemShop.Discount = request.ShopDiscount;
            }

            return magicItemShop;
        }

        private static Dictionary<MagicItemRarity, (int min, int max)> GetRarityQuantityRanges(MagicItemShopRarityQuantityMultipliers multipliers)
        {
            return multipliers
                .ToDictionary(x => x.Key, x => (min: (int)(x.Value / 2), max: (int)x.Value));
        }
    }
}