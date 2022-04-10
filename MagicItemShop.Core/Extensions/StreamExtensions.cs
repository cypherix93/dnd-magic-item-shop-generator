namespace MagicItemShop.Core.Extensions
{
    public static class StreamExtensions
    {
        public static string ReadAsString(this Stream stream)
        {
            var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }

        public static async Task<string> ReadAsStringAsync(this Stream stream)
        {
            var reader = new StreamReader(stream);

            return await reader.ReadToEndAsync();
        }
    }
}