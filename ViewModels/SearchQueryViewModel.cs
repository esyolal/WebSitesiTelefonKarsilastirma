using Karsilastirma.Models;
using System.Collections.Generic;

namespace Karsilastirma.ViewModels
{
    public class SearchQueryViewModel
    {
        public Request Request { get; set; }
        public IList<Product> Products { get; set; }
    }
}
