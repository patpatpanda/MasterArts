using MasterArtsLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace MasterArtsWeb.Pages.LogisticsCenter
{
    public class CalculatorModel : BaseModel
    {
       

        public CalculatorModel(LanguageService languageService, IHttpClientFactory clientFactory)
            : base(languageService)
        {
            
        }

        
        

       

       

        public void OnGetCalculator()
        {
          
        }

        public void OnPostCalculator()
        {
            

           
        }
    }
}



