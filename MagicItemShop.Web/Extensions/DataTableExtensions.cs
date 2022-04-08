#nullable enable
using System.Data;
using Newtonsoft.Json;

namespace MagicItemShop.Web.Extensions
{
    public static class DataTableExtensions
    {
        public static IList<T> ToObjectsList<T>(this DataTable dataTable)
        {
            return JsonConvert.DeserializeObject<IList<T>>(JsonConvert.SerializeObject(dataTable))!;
        }

        public static DataTable ToDataTable<TValue>(this IEnumerable<IDictionary<string, TValue>> data, string? tableName = null)
        {
            var dataJson = JsonConvert.SerializeObject(data);
            var dataTable = JsonConvert.DeserializeObject<DataTable>(dataJson)!;

            dataTable.TableName = tableName;
            return dataTable;
        }
    }
}