using MasterArtsLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MasterArtsWeb.Pages
{
    public class ArtServicesModel : BaseModel
    {


        public ArtServicesModel(LanguageService languageService)
            : base(languageService)
        {

        }

        public void OnGetArt()
        {

        }


    }
}
