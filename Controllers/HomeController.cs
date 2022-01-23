using Karsilastirma.Business;
using Karsilastirma.Models;
using Karsilastirma.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Karsilastirma.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRequestSite _requestSite;

        public HomeController(IRequestSite requestSite)
        {
            _requestSite = requestSite;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("search", Name = "Search")]
        public IActionResult Search(SearchQueryViewModel model)
        {
            model.Products = _requestSite.GetAllProductByQuery(model.Request);
            return View(model);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
