//using MasterArtsLibrary.ViewModels;
//using System;
//using System.Net.Http;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace MasterArtsLibrary.Services
//{
//    public class CurrencyFactory
//    {
//        private readonly HttpClient _httpClient;

//        public CurrencyFactory(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//        }

//        public async Task<ExchangeRateModel> GetExchangeRateAsync(string baseCurrency, string targetCurrency)
//        {
//            try
//            {
//                var apiUrl = $"https://api.exchangerate-api.com/v4/latest/{baseCurrency}";
//                var response = await _httpClient.GetAsync(apiUrl);
//                response.EnsureSuccessStatusCode();
//                var jsonResponse = await response.Content.ReadAsStringAsync();
//                var exchangeRateData = JsonSerializer.Deserialize<ExchangeRateApiResponse>(jsonResponse);

//                if (exchangeRateData.Rates != null && exchangeRateData.Rates.ContainsKey(targetCurrency))
//                {
//                    var exchangeRate = exchangeRateData.Rates[targetCurrency];

//                    return new ExchangeRateModel
//                    {
//                        BaseCurrency = baseCurrency,
//                        TargetCurrency = targetCurrency,
//                        Rate = exchangeRate
//                    };
//                }
//                else
//                {
//                    Console.WriteLine($"Exchange rate for {targetCurrency} not found in response: {jsonResponse}");
//                    return null;
//                }
//            }
//            catch (HttpRequestException ex)
//            {
//                Console.WriteLine($"Error getting exchange rate data: {ex.Message}");
//                return null;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
//                return null;
//            }
//        }



       

//    }
//}
