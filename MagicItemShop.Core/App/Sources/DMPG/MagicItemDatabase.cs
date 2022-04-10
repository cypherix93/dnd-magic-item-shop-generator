using MagicItemShop.Core.App.Sources.DMPG.Models;
using MagicItemShop.Core.Extensions;
using MagicItemShop.Core.Resources;
using Newtonsoft.Json;

namespace MagicItemShop.Core.App.Sources.DMPG
{
    public static class MagicItemDatabase
    {
        public static readonly IReadOnlyList<MagicItem> Items;

        static MagicItemDatabase()
        {
            var dmpgItemsResourceContents = MagicItemShopResources.GetDMPGItemsResource().ReadAsString();

            Items = JsonConvert.DeserializeObject<List<MagicItem>>(dmpgItemsResourceContents)!;
        }
    }
}