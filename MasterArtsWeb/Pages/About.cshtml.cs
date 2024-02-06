using MasterArtsLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MasterArtsWeb.Pages
{
    public class AboutModel : BaseModel
    {



        public AboutModel(LanguageService languageService)
            : base(languageService)
        {

        }

        public void OnGetAbout()
        {
        }
    }
}
