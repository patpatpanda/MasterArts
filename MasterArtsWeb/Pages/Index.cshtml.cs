using ClosedXML.Excel;
using MasterArtsLibrary.Models;
using MasterArtsLibrary.Services;
using MasterArtsLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using CsvReader = CsvHelper.CsvReader;

// Andra 'using'-satser...


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
            //string filePath = @"C:\Users\Nils-\OneDrive\Skrivbord\airports (2).csv";


            //var airports = await ReadAirportsFromCsv(filePath);

            
            //await AddAirportsToDbAsync(airports, _context);




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

        

    ////    public async Task<List<Airport>> ReadAirportsFromCsv(string filePath)
    ////    {
    ////        var airports = new List<Airport>();

    ////        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
    ////        {
    ////            HasHeaderRecord = true, // Sätt till false om din CSV inte har en header
    ////        };

    ////        using (var reader = new StreamReader(filePath))
    ////        using (var csv = new CsvReader(reader, config))
    ////        {
    ////            csv.Context.RegisterClassMap<AirportMap>(); // Använd denna om du har en komplex mappning
    ////            airports = csv.GetRecords<Airport>().ToList();
    ////        }

    ////        return airports;
    ////    }
    ////    public async Task AddAirportsToDbAsync(List<Airport> airports, MyDbContext context)
    ////    {
    ////        foreach (var airport in airports)
    ////        {
    ////            context.Airports.Add(airport);
    ////        }
    ////        await context.SaveChangesAsync();
    ////    }
    ////}

    

}
////public class AirportMap : ClassMap<Airport>
////{
////    public AirportMap()
////    {
////        // Map columns to properties based on your CSV structure
////        Map(m => m.Ident).Index(1); // Kom ihåg att indexeringen börjar på 0
////        Map(m => m.Type).Index(2);
////        Map(m => m.Name).Index(3);
////        Map(m => m.LatitudeDeg).Index(4);
////        Map(m => m.LongitudeDeg).Index(5);
////        Map(m => m.ElevationFt).Index(6);
////        Map(m => m.Continent).Index(7);
////        Map(m => m.IsoCountry).Index(8);
////        Map(m => m.IsoRegion).Index(9);
////        Map(m => m.Municipality).Index(10);
////        Map(m => m.ScheduledService).Index(11);
////        Map(m => m.GpsCode).Index(12);
////        Map(m => m.IataCode).Index(13);
////        Map(m => m.LocalCode).Index(14);
////        Map(m => m.HomeLink).Index(15);
////        Map(m => m.WikipediaLink).Index(16);
////        Map(m => m.Keywords).Index(17);
////        // Fortsätt att mappa alla nödvändiga fält ...
////    }
}