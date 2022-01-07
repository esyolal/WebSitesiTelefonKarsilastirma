using Microsoft.AspNetCore.Mvc;
using Karsilastirma.Models;
using Karsilastirma.Business;

namespace Karsilastirma.Controllers
{
    public class vatanController : Controller
    {
        IRequestSite _requestSite;

        public vatanController(IRequestSite requestSite)
        {
            _requestSite = requestSite;
        }

        public ActionResult Index()
        {
            var Vatanrequest = new Request { SiteUrl = "https://www.vatanbilgisayar.com/", Query = "cep-telefonu-modelleri/" };
            var result = _requestSite.GetProductsByVatanBilgisayar(Vatanrequest);
            
            return View(result);
        }

    }
}

