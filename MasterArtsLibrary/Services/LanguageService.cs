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
            // Hämta användarens språkpreferens från local storage eller cookie
            return _httpContextAccessor.HttpContext.Request.Cookies["language"] ?? "en";
        }

        public string ToggleLanguage()
        {
            // Hämta värdet från cookien
            string currentLanguageCookie;
            _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("language", out currentLanguageCookie);

            // Om cookien inte finns, sätt till standardvärdet "en"
            string newLanguage = currentLanguageCookie ?? "en";

            // Växla mellan svenska och engelska
            newLanguage = newLanguage == "en" ? "sv" : "en";

            // Spara det nya språket i cookien
            _httpContextAccessor.HttpContext.Response.Cookies.Append("language", newLanguage);

            return newLanguage;
        }
    }
}
