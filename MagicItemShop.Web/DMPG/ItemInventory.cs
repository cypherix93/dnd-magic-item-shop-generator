using MagicItemShop.Web.DMPG.Models;
using MagicItemShop.Web.Extensions;
using MagicItemShop.Web.GoogleSheets;
using Newtonsoft.Json;

namespace MagicItemShop.Web.DMPG
{
    public static class ItemInventory
    {
        private static IList<MagicItem> _items;
        private const string DMPG_ITEMS_FILE = "Resources/dmpg_items.json";

        private static readonly object ITEMS_LOCK = new();
        private static readonly SemaphoreSlim ITEMS_SEMAPHORE = new(1, 1);

        public static async Task<IList<MagicItem>> GetItemsFromDMPG()
        {
            if (_items is not null)
            {
                return _items;
            }

            await ITEMS_SEMAPHORE.WaitAsync();

            try
            {
                if (File.Exists(DMPG_ITEMS_FILE))
                {
                    var itemsFileContents = await File.ReadAllTextAsync(DMPG_ITEMS_FILE);

                    _items = JsonConvert.DeserializeObject<IList<MagicItem>>(itemsFileContents);

                    return _items;
                }

                var itemsDataSet = await GoogleSheetsApi.GetGoogleSheetData(GoogleSheetsApi.Sheets["DMPG_MagicalItemPrices"], new[] { "Magic Items" });

                _items = itemsDataSet.Tables[0].ToObjectsList<MagicItem>();

                await File.WriteAllTextAsync(DMPG_ITEMS_FILE, JsonConvert.SerializeObject(_items));

                return _items;
            }
            finally
            {
                ITEMS_SEMAPHORE.Release();
            }
        }
    }
}