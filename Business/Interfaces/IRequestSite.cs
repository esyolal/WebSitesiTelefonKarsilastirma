using Karsilastirma.Models;
using System.Collections.Generic;

namespace Karsilastirma.Business
{
    public interface IRequestSite
    {
        List<Product> GetProductsByVatanBilgisayar(Request request);
        List<Product> GetProductsByGittiGidiyor(Request request);
        List<Product> GetProductsByTeknosa(Request request);
        List<Product> GetAllProductByQuery(Request request);
    }
}
