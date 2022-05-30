#nullable enable
using MagicItemShop.Core.App.MagicItemShop.Models.Common;
using MagicItemShop.Core.App.Sources.DMPG.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MagicItemShop.Core.Json
{
    public class MagicItemTypeConverter : StringEnumConverter
    {
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null || reader.Value is null)
            {
                return null;
            }

            var itemTypeString = (string)reader.Value;

            if (itemTypeString.Contains("Potion", StringComparison.OrdinalIgnoreCase))
            {
                return MagicItemType.PotionOil;
            }

            if (itemTypeString.Contains("Ring", StringComparison.OrdinalIgnoreCase))
            {
                return MagicItemType.Ring;
            }

            if (itemTypeString.Contains("Spell Gems", StringComparison.OrdinalIgnoreCase))
            {
                return MagicItemType.SpellGem;
            }

            if (itemTypeString.Contains("Spell Scrolls", StringComparison.OrdinalIgnoreCase))
            {
                return MagicItemType.SpellScroll;
            }

            if (itemTypeString.Contains("Wondrou", StringComparison.OrdinalIgnoreCase))
            {
                return MagicItemType.WondrousItem;
            }

            var matchingEnumName = Enum.GetNames<MagicItemType>()
                .FirstOrDefault(x => itemTypeString.Contains(x));

            if (matchingEnumName is not null && Enum.TryParse<MagicItemType>(matchingEnumName, out var magicItemType))
            {
                return magicItemType;
            }

            return null;
        }
    }
}