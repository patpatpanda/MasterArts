using MasterArtsLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MasterArtsWeb.Pages
{
    public class TermsModel : BaseModel
    {


        public TermsModel(LanguageService languageService)
            : base(languageService)
        {

        }
    
        public void OnGetTerms()
        {
        }
    }
}
