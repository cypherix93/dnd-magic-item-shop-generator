using MagicItemShop.Core.App.Sources.DMPG.Models;
using MagicItemShop.Core.Extensions;
using Newtonsoft.Json;

#nullable enable

namespace MagicItemShop.Core.App.MagicItemShop.Models
{
    public class MagicItemShop
    {
        private readonly IEnumerable<MagicItemShopItem> _items;

        public string Name { get; }

        public decimal Discount { get; set; }

        public MagicItemShop(IEnumerable<MagicItemShopItem> items, string name)
        {
            _items = items;
            Name = name;
        }

        public List<MagicItemShopItem> Inventory => _items
            .Select(item =>
            {
                item.AppliedDiscount = Discount;

                return item;
            })
            .ToList();
    }

    public class MagicItemShopItem
    {
        private readonly MagicItem _item;

        [JsonIgnore]
        public decimal AppliedDiscount { get; set; }

        public MagicItemShopItem(MagicItem item)
        {
            _item = item;
        }

        [JsonProperty(Order = -100)]
        public string Name => _item.ItemName;

        [JsonProperty(Order = -99)]
        public string Url => _item.ItemUrl;

        [JsonProperty(Order = -90)]
        public MagicItemType Type => _item.Type;

        [JsonProperty(Order = -80)]
        public MagicItemRarity Rarity => _item.Rarity;

        [JsonProperty(Order = -70)]
        public bool RequiresAttunement => _item.Attunement?.Equals("Yes", StringComparison.OrdinalIgnoreCase) ?? false;

        [JsonProperty(Order = -60)]
        public SourceBook Source => _item.Source;

        [JsonProperty(Order = 100)]
        public int Price
        {
            get
            {
                var basePrice = GetItemPrice();

                return (int)Math.Round(basePrice * (1m - AppliedDiscount));
            }
        }

        private int GetItemPrice()
        {
            int price;
            if (int.TryParse(_item.SanePrice, out price) || int.TryParse(_item.DMPGPrice, out price))
            {
                return price;
            }

            var priceRangeSource = new[] { _item.DMGPrice, _item.XGEPrice }.PickRandom();

            return PickPriceFromRange(priceRangeSource);
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