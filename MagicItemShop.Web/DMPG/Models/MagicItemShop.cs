using MagicItemShop.Web.Extensions;
using Newtonsoft.Json;

#nullable enable

namespace MagicItemShop.Web.DMPG.Models
{
    public class MagicItemShop
    {
        public string Name { get; set; }

        public List<MagicItemShopItem> Inventory { get; set; }

        public decimal Discount { get; set; } = 0m;

        public MagicItemShop(string name, List<MagicItemShopItem> inventory)
        {
            Name = name;
            Inventory = inventory;
        }
    }

    public class MagicItemShopItem
    {
        private readonly MagicItem _item;

        public MagicItemShopItem(MagicItem item)
        {
            _item = item;
        }

        [JsonProperty(Order = -100)]
        public string Name => _item.Item;

        [JsonProperty(Order = -90)]
        public MagicItemType? Type => _item.Type;

        [JsonProperty(Order = -80)]
        public MagicItemRarity Rarity => _item.Rarity;

        [JsonProperty(Order = -70)]
        public bool RequiresAttunement => _item.Attunement?.Equals("Yes", StringComparison.OrdinalIgnoreCase) ?? false;

        public int Price
        {
            get
            {
                int price;
                if (int.TryParse(_item.SanePrice, out price) || int.TryParse(_item.DMPGPrice, out price))
                {
                    return price;
                }

                var priceRangeSource = new[] { _item.DMGPrice, _item.XGEPrice }.PickRandom();

                return PickPriceFromRange(priceRangeSource);
            }
        }

        private static int PickPriceFromRange(string priceRange)
        {
            var random = new Random();

            var range = priceRange
                .Split("-")
                .Select(x => int.TryParse(x, out var price) ? price : 0)
                .ToArray();

            if (range.Length < 2)
            {
                return 0;
            }

            return (int)(Math.Round((decimal)random.Next(range[0], range[1]) / 50, 0) * 50);
        }
    }
}