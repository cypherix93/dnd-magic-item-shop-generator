using MagicItemShop.Web.DMPG.Models;
using Newtonsoft.Json;

namespace MagicItemShop.Web.DMPG
{
    public static class MagicItemDatabase
    {
        private const string DMPG_ITEMS_FILE = "Resources/dmpg_items.json";

        private static readonly object ITEMS_SEMAPHORE = new();

        public static readonly IReadOnlyList<MagicItem> Items;

        static MagicItemDatabase()
        {
            lock (ITEMS_SEMAPHORE)
            {
                if (!File.Exists(DMPG_ITEMS_FILE))
                {
                    throw new InvalidDataException($"Items file does not exist. Path: {DMPG_ITEMS_FILE}");
                }

                var itemsFileContents = File.ReadAllText(DMPG_ITEMS_FILE);
                
                Items = JsonConvert.DeserializeObject<List<MagicItem>>(itemsFileContents);
            }
        }
    }
}