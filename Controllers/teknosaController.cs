using Karsilastirma.Business;
using Karsilastirma.Models;
using Microsoft.AspNetCore.Mvc;

namespace Karsilastirma.Controllers
{
    public class teknosaController : Controller
    {
        IRequestSite _requestSite;

        public teknosaController(IRequestSite requestSite)
        {
            _requestSite = requestSite;
        }

        public ActionResult Index()
        {
            var teknosaRequest = new Request { SiteUrl = "https://www.teknosa.com/", Query = "arama/?s=cep+telefonları" };
            var result = _requestSite.GetProductsByTeknosa(teknosaRequest);

            return View(result);
        }

    }
}
