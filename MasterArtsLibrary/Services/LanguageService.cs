using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Services
{
    public class LanguageService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public LanguageService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentLanguage()
        {
            
            return _httpContextAccessor.HttpContext.Request.Cookies["language"] ?? "en";
        }

        public string ToggleLanguage()
        {
            
            string currentLanguageCookie;
            _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("language", out currentLanguageCookie);

            
            string newLanguage = currentLanguageCookie ?? "en";

           
            newLanguage = newLanguage == "en" ? "sv" : "en";

            
            _httpContextAccessor.HttpContext.Response.Cookies.Append("language", newLanguage);

            return newLanguage;
        }
    }
}
