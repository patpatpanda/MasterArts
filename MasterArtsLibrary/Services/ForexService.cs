using MasterArtsLibrary.ViewModels;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace YourNamespace
{
    public class ForexService
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "https://api.fastforex.io";

        public ForexService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ForexApiResponse> FetchAll(string apiKey)
        {
            var apiUrl = $"{ApiBaseUrl}/fetch-all?api_key={apiKey}";

            var response = await _httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                // Handle error here, e.g., log or throw exception
                throw new Exception($"Failed to fetch data from API. Status code: {response.StatusCode}");
            }

            var jsonContent = await response.Content.ReadAsStringAsync();
            var forexApiResponse = JsonSerializer.Deserialize<ForexApiResponse>(jsonContent);

            return forexApiResponse;
        }

    }


}
