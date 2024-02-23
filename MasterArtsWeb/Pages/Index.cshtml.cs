using MasterArtsLibrary.Models;
using MasterArtsLibrary.Services;
using MasterArtsLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace MasterArtsWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly LanguageService _languageService;
        private readonly IHttpClientFactory _clientFactory;
        private readonly MyDbContext _context;
        public Dictionary<string, decimal> PreviousRates { get; private set; }



        public IndexModel(LanguageService languageService, IHttpClientFactory clientFactory,MyDbContext context)
        {
            _languageService = languageService;
            _clientFactory = clientFactory;
            _context = context;
            
        }

       
        public string WeatherDescription { get; private set; }
        public string CurrentLanguage { get; set; }
        public ExchangeRateModel ExchangeRate { get; private set; }
        public CurrencyExchangeRates CurrencyData { get; set; }
        public string BaseCurrency { get; set; } = "SEK";
      
       
           public async Task<IActionResult> OnGetAsync()
        {
            PreviousRates = await GetPreviousRates(DateTime.Now);
            CurrentLanguage = _languageService.GetCurrentLanguage();
            ViewData["Language"] = CurrentLanguage;

          

            var client = _clientFactory.CreateClient();

           
            var currencyResponse = await client.GetStringAsync($"https://api.exchangerate-api.com/v4/latest/{BaseCurrency}");
            CurrencyData = JsonConvert.DeserializeObject<CurrencyExchangeRates>(currencyResponse);

            
            var exchangeRateResponse = await client.GetStringAsync($"https://api.exchangerate-api.com/v4/latest/{BaseCurrency}");
            ExchangeRate = JsonConvert.DeserializeObject<ExchangeRateModel>(exchangeRateResponse);


            var mrRate = new MrRate
            {
                Provider = CurrencyData.Provider,
                Base = CurrencyData.Base,
                Date = CurrencyData.Date
            };
            _context.Rates.Add(mrRate);
            _context.SaveChanges();
            foreach (var rate in ExchangeRate.Rates)
            {
                var exchangeRate = new ExchangeRate
                {
                    BaseCurrency = ExchangeRate.BaseCurrency,
                    TargetCurrency = rate.Key,
                    Rate = rate.Value
                };
                _context.CurrencyRates.Add(exchangeRate);
                _context.SaveChanges();
            }
            return Page();
        }
    
    public IActionResult OnPost()
        {
            CurrentLanguage = _languageService.ToggleLanguage();
            ViewData["Language"] = CurrentLanguage;
            Console.WriteLine($"Language switched to: {CurrentLanguage}");
            return RedirectToPage();
        }
        public async Task<Dictionary<string, decimal>> GetPreviousRates(DateTime currentDate)
        {
            var previousDate = await _context.Rates
                                    .Where(r => r.Date < currentDate)
                                    .OrderByDescending(r => r.Date)
                                    .Select(r => r.Date)
                                    .FirstOrDefaultAsync();

            if (previousDate == default(DateTime))
            {
                return new Dictionary<string, decimal>();
            }

            var previousRates = await _context.CurrencyRates
                .Join(_context.Rates,
                      currencyRate => currencyRate.Id, // Antag att det finns en MrRateId i ExchangeRate
                      rate => rate.Id,
                      (currencyRate, rate) => new { currencyRate, rate })
                .Where(x => x.rate.Date.Date == previousDate.Date)
                .ToDictionaryAsync(x => x.currencyRate.TargetCurrency, x => x.currencyRate.Rate.Value);

            return previousRates;
        }

        public double CalculatePercentageChange(decimal previousRate, decimal currentRate)
        {
            if (previousRate == 0) return 0;
            return (double)((currentRate - previousRate) / previousRate * 100);
        }



    }
}
