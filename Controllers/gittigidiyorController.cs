using Karsilastirma.Business;
using Karsilastirma.Models;
using Microsoft.AspNetCore.Mvc;

namespace Karsilastirma.Controllers
{
    public class gittigidiyorController : Controller
    {
        IRequestSite _requestSite;

        public gittigidiyorController(IRequestSite requestSite)
        {
            _requestSite = requestSite;
        }
        public ActionResult Index()
        {
            var gittiRequest = new Request { SiteUrl = "https://www.gittigidiyor.com/", Query = "cep-telefonu/" };
            var result = _requestSite.GetProductsByGittiGidiyor(gittiRequest);
            return View(result);
        }
    }
}
