#nullable enable

namespace MagicItemShop.Core.App.MagicItemShop.Models
{
    public class MagicShop
    {
        private readonly IEnumerable<MagicItem> _items;

        public string Name { get; }

        public decimal Discount { get; set; }

        public MagicShop(IEnumerable<MagicItem> items, string name)
        {
            _items = items;
            Name = name;
        }

        public List<MagicItem> Inventory => _items
            .Select(item =>
            {
                item.AppliedDiscount = Discount;

                return item;
            })
            .ToList();
    }
}