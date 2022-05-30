using System.Runtime.Serialization;
using MagicItemShop.Core.App.MagicItemShop.Models.Common;
using MagicItemShop.Core.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MagicItemShop.Core.App.Sources.DMPG.Models
{
    public class DMPGMagicItem
    {
        [JsonProperty("Item")]
        public string ItemName { get; set; }

        public string ItemUrl { get; set; }

        public string SanePrice { get; set; }
        public string DMPGPrice { get; set; }
        public string MBJVPrice { get; set; }
        public string DMGPrice { get; set; }
        public string XGEPrice { get; set; }

        public MagicItemRarity Rarity { get; set; }
        public SourceBook Source { get; set; }
        public string Page { get; set; }

        public MagicItemType Type { get; set; }

        public string Attunement { get; set; }
    }
}