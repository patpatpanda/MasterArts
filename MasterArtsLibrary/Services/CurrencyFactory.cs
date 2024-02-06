using MasterArtsLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Services
{
    public class CurrencyFactory
    {
        private readonly HttpClient _httpClient;

        public CurrencyFactory(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ExchangeRateModel> GetExchangeRateAsync(string baseCurrency, string targetCurrency)
        {
            var apiUrl = $"https://api.exchangerate-api.com/v4/latest/{baseCurrency}";
            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var exchangeRateData = JsonSerializer.Deserialize<ExchangeRateApiResponse>(jsonResponse);

            if (exchangeRateData.Rates != null && exchangeRateData.Rates.ContainsKey(targetCurrency))
            {
                var exchangeRate = exchangeRateData.Rates[targetCurrency];


                return new ExchangeRateModel
                {
                    BaseCurrency = baseCurrency,
                    TargetCurrency = targetCurrency,
                    Rate = exchangeRate
                };
            }
            else
            {
                // Valutakoden "USD" finns inte i responsen, logga det och hantera det
                Console.WriteLine($"Exchange rate for {targetCurrency} not found in response: {jsonResponse}");

                // Alternativt kan du använda något standardvärde eller returnera null
                return null;
            }
        }


    }
}
