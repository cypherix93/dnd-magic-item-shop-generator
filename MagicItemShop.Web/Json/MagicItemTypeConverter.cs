#nullable enable
using MagicItemShop.Web.DMPG.Models;
using Newtonsoft.Json;

namespace MagicItemShop.Web.Json
{
    public class MagicItemTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            writer.WriteValue(value?.ToString());
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null || reader.Value is null)
            {
                return null;
            }

            var itemTypeString = (string)reader.Value;

            if (Enum.TryParse<MagicItemType>(itemTypeString, ignoreCase: true, out var magicItemType))
            {
                return magicItemType;
            }

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

            return null;
        }
    }
}