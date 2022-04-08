using System.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;

namespace MagicItemShop.Web.GoogleSheets
{
    public static class GoogleSheetsApi
    {
        private static readonly string _credentialsFilePath = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), @".config\gcloud\sheets\client_secret_340827405659-sgjufob44a82on45dp9ajneo6po9kftt.apps.googleusercontent.com.json");
        private static readonly string _authTokenFilePath = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), @".config\gcloud\sheets\token.json");

        private static readonly string[] _scopes = { SheetsService.Scope.SpreadsheetsReadonly };

        private static SheetsService _service;

        public static readonly Dictionary<string, string> Sheets = new()
        {
            { "DMPG_MagicalItemPrices", "1OG7UsbsjNFX4zVkDORiem1ySUGYrhu-wrTRnGEk4jgc" }
        };

        public static async Task<DataSet> GetGoogleSheetData(string sheetId, string[] ranges)
        {
            //if (_service is null)
            //{
            await InitApi();
            //}

            var request = _service.Spreadsheets.Values.BatchGet(sheetId);
            request.Ranges = ranges;
            request.ValueRenderOption = SpreadsheetsResource.ValuesResource.BatchGetRequest.ValueRenderOptionEnum.UNFORMATTEDVALUE;
            request.DateTimeRenderOption = SpreadsheetsResource.ValuesResource.BatchGetRequest.DateTimeRenderOptionEnum.FORMATTEDSTRING;

            var response = await request.ExecuteAsync();

            var dataSet = new DataSet();

            if (!response.ValueRanges.Any())
            {
                return dataSet;
            }

            var tables = response.ValueRanges
                .Where(x => x.Values.Any())
                .Select(x =>
                {
                    var table = new DataTable();
                    table.TableName = x.Range.Split("!").FirstOrDefault();

                    // columns
                    table.Columns.AddRange(
                        x.Values[0].Select(x => new DataColumn(x.ToString())).ToArray()
                    );

                    // rows
                    foreach (var valueRow in x.Values.Skip(1))
                    {
                        table.Rows.Add(valueRow.ToArray());
                    }

                    return table;
                })
                .ToArray();

            dataSet.Tables.AddRange(tables);

            return dataSet;
        }

        private static async Task InitApi()
        {
            await using var credentialsFileStream = File.OpenRead(_credentialsFilePath);

            var secrets = GoogleClientSecrets.FromStream(credentialsFileStream);

            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(secrets.Secrets, _scopes, "bik", CancellationToken.None, new FileDataStore(_authTokenFilePath, true));

            // Create Google Sheets API service.
            _service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "LINQPad",
            });
        }
    }
}