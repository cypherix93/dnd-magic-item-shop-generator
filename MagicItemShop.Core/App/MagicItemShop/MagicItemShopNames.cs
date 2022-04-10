using MagicItemShop.Core.Extensions;
using MagicItemShop.Core.Resources;
using Newtonsoft.Json;

namespace MagicItemShop.Core.App.MagicItemShop
{
    public static class MagicItemShopNames
    {
        private static readonly ShopNamesData _shopNamesData;

        static MagicItemShopNames()
        {
            var shopNamesResourceContents = MagicItemShopResources.GetShopNamesResource().ReadAsString();

            _shopNamesData = JsonConvert.DeserializeObject<ShopNamesData>(shopNamesResourceContents)!;
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