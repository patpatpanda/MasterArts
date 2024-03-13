using ClosedXML.Excel;
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



        public IndexModel(LanguageService languageService, IHttpClientFactory clientFactory, MyDbContext dbContext)
        {
            _languageService = languageService;
            _clientFactory = clientFactory;
            _context = dbContext;


        }


        public string WeatherDescription { get; private set; }
        public string CurrentLanguage { get; set; }
        public ExchangeRateModel ExchangeRate { get; private set; }
        public CurrencyExchangeRates CurrencyData { get; set; }
        public string BaseCurrency { get; set; } = "SEK";


        public async Task<IActionResult> OnGetAsync()
        {
    //         string filePath = @"C:\Users\Nils-\OneDrive\Skrivbord\Bok2.txt.xlsx";
    
    
    //var airports = ReadAirportsFromExcel(filePath);

    
    //await AddAirportsAsync(airports);
            CurrentLanguage = _languageService.GetCurrentLanguage();
            ViewData["Language"] = CurrentLanguage;



            var client = _clientFactory.CreateClient();


            var currencyResponse = await client.GetStringAsync($"https://api.exchangerate-api.com/v4/latest/{BaseCurrency}");
            CurrencyData = JsonConvert.DeserializeObject<CurrencyExchangeRates>(currencyResponse);


            var exchangeRateResponse = await client.GetStringAsync($"https://api.exchangerate-api.com/v4/latest/{BaseCurrency}");
            ExchangeRate = JsonConvert.DeserializeObject<ExchangeRateModel>(exchangeRateResponse);



            return Page();
        }

        public IActionResult OnPost()
        {
            CurrentLanguage = _languageService.ToggleLanguage();
            ViewData["Language"] = CurrentLanguage;
            Console.WriteLine($"Language switched to: {CurrentLanguage}");
            return RedirectToPage();
        }

        public List<Airport> ReadAirportsFromExcel(string filePath)
        {
            var airports = new List<Airport>();
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RangeUsed().RowsUsed().Skip(1); 

                foreach (var row in rows)
                {
                    var airport = new Airport
                    {
                        Ident = row.Cell(2).GetValue<string>(), 
                        Type = row.Cell(3).GetValue<string>(),
                        Name = row.Cell(4).GetValue<string>(),
                        LatitudeDeg = row.Cell(5).GetValue<string>(), 
                        LongitudeDeg = row.Cell(6).GetValue<string>(), 
                        ElevationFt = row.Cell(7).GetValue<int?>(),
                        Continent = row.Cell(8).GetValue<string>(),
                        IsoCountry = row.Cell(9).GetValue<string>(),
                        IsoRegion = row.Cell(10).GetValue<string>(),
                        Municipality = row.Cell(11).GetValue<string>(),
                        ScheduledService = row.Cell(12).GetValue<string>(),
                        GpsCode = row.Cell(13).GetValue<string>(),
                        IataCode = row.Cell(14).GetValue<string>(),
                        LocalCode = row.Cell(15).GetValue<string>(),
                        HomeLink = row.Cell(16).GetValue<string>(),
                        WikipediaLink = row.Cell(17).GetValue<string>(),
                        Keywords = row.Cell(18).GetValue<string>(), // Ensure you're getting the right cell index
                    };

                    airports.Add(airport);
                }
            }
            return airports;
        }

        public async Task AddAirportsAsync(List<Airport> airports)
        {

            {
                foreach (var airport in airports)
                {
                    _context.Airports.Add(airport);
                }
                await _context.SaveChangesAsync();
            }
        }


    }



}
