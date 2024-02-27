﻿using MasterArtsLibrary.Models;
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
      
        public Dictionary<string, decimal> PreviousRates { get; private set; }



        public IndexModel(LanguageService languageService, IHttpClientFactory clientFactory)
        {
            _languageService = languageService;
            _clientFactory = clientFactory;
           
            
        }

       
        public string WeatherDescription { get; private set; }
        public string CurrentLanguage { get; set; }
        public ExchangeRateModel ExchangeRate { get; private set; }
        public CurrencyExchangeRates CurrencyData { get; set; }
        public string BaseCurrency { get; set; } = "SEK";
      
       
           public async Task<IActionResult> OnGetAsync()
        {
           
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
       



    }
}
