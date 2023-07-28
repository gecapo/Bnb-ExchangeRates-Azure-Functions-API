using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace UnnamedProject
{
    public class BnbClient : IBnbClient
    {
        private readonly string _bnbUrl = "https://www.bnb.bg";

        public async Task<string> GetExchangeRates(DateTime dateTime)
        {
            using var httpClient = new HttpClient() { BaseAddress = new Uri(_bnbUrl) };
            var query = string.Empty;
            using (var content = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    { "downloadOper", "true"},
                    { "group1", "first"},
                    { "firstDays", dateTime.Day.ToString()},
                    { "firstMonths", dateTime.Month.ToString()},
                    { "firstYear", dateTime.Year.ToString()},
                    { "search", "true"},
                    { "showChart", "false"},
                    { "showChartButton", "false"},
                    { "type", "XML"}
                })) { query = content.ReadAsStringAsync().Result; }

            var url = $"Statistics/StExternalSector/StExchangeRates/StERForeignCurrencies/index.htm?{query}";
            var response = await httpClient.GetStringAsync(url);
            return response;
        }
    }
}

