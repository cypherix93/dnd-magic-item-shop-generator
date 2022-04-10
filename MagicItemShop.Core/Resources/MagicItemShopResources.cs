using System.Reflection;

namespace MagicItemShop.Core.Resources
{
    public static class MagicItemShopResources
    {
        private static string ResourcesNamespace => $"{typeof(MagicItemShopResources).Namespace}.Data";

        private static readonly Dictionary<string, string> ResourceNames = new()
        {
            { "dmpg_items", "dmpg_items.json" },
            { "shopnames", "shopnames.json" }
        };

        public static Stream GetDMPGItemsResource()
        {
            return Assembly.GetAssembly(typeof(MagicItemShopResources))!.GetManifestResourceStream($"{ResourcesNamespace}.{ResourceNames["dmpg_items"]}")!;
        }

        public static Stream GetShopNamesResource()
        {
            return Assembly.GetAssembly(typeof(MagicItemShopResources))!.GetManifestResourceStream($"{ResourcesNamespace}.{ResourceNames["shopnames"]}")!;
        }
    }
}