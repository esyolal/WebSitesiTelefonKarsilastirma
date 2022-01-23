using HtmlAgilityPack;
using Karsilastirma.Models;
using System.Collections.Generic;
using System.Linq;

namespace Karsilastirma.Business
{
    public class RequestSite : IRequestSite
    {
        ISendRequest _sendRequest;

        public RequestSite(ISendRequest sendRequest)
        {
            _sendRequest = sendRequest;
        }

        public List<Product> GetAllProductByQuery(Request request)
        {
            var vatanQuery= $"/arama/{request.Query.Trim().Replace(" ", "%20")}/cep-telefonu-modelleri/";
            var gittigidiyorQuery= $"/cep-telefonu?k={request.Query.Trim().Replace(" ", "%20")}/";
            var teknosaQuery= $"arama/?s={request.Query.Trim().Replace(" ", "%20")}";
            List<Product> products = new List<Product>();
            request.SiteUrl = "https://www.vatanbilgisayar.com/";
            request.Query = vatanQuery;
            var vatanProducts = GetProductsByVatanBilgisayar(request);
            foreach (var product in vatanProducts)
            {
                products.Add(new Product
                {
                    Name = product.Name,
                    PhotoUrl = product.PhotoUrl,
                    Price = product.Price,
                    ProductCode = product.ProductCode,
                    Url = product.Url,
                    Store = "Vatan"
                });
            }
            request.SiteUrl = "https://www.gittigidiyor.com/";
            request.Query = gittigidiyorQuery;
            var gittiGidiyorProducts = GetProductsByGittiGidiyor(request);
            foreach (var product in gittiGidiyorProducts)
            {
                products.Add(new Product
                {
                    Name = product.Name,
                    PhotoUrl = product.PhotoUrl,
                    Price = product.Price,
                    ProductCode = product.ProductCode,
                    Url = product.Url,
                    Store = "GittiGidiyor"
                });

            }
            request.SiteUrl = "https://www.teknosa.com/";
            request.Query = teknosaQuery;
            var teknosaProducts = GetProductsByTeknosa(request);
            foreach (var product in teknosaProducts)
            {
                products.Add(new Product
                {
                    Name = product.Name,
                    PhotoUrl = product.PhotoUrl,
                    Price = product.Price,
                    ProductCode = product.ProductCode,
                    Url = product.Url,
                    Store = "Teknosa"

                });
            }

            return products.OrderBy(m => m.Price).ToList();


        }

        public List<Product> GetProductsByVatanBilgisayar(Request request)
        {
            var getHtml = _sendRequest.GetList(request);
            var sec = getHtml.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Contains("product-list product-list--list-page")).ToList();
            List<Product> urunler = new List<Product>();

            foreach (var link in sec)
            {

                var isim = link.SelectSingleNode(".//div[@class='product-list__product-name']").InnerText.Trim();
                var fiyat = link.SelectSingleNode(".//span[@class='product-list__price']").InnerText.Trim();
                var foto = link.SelectSingleNode(".//img[@class='lazyimg']").Attributes["data-src"].Value;
                var urunlink = "https://www.vatanbilgisayar.com" + link.SelectSingleNode(".//a[@class='product-list__link']").Attributes["href"].Value;
                urunler.Add(new vatanUrun()
                {
                    Price = double.Parse(fiyat),
                    Name = isim,
                    PhotoUrl = foto,
                    Url = urunlink
                });

            }

            return urunler;
        }
        public List<Product> GetProductsByGittiGidiyor(Request request)
        {
            var getHtml = _sendRequest.GetList(request);
            var sec = getHtml.DocumentNode.Descendants("li").Where(node => node.GetAttributeValue("class", "").Contains("list-item")).ToList();
            List<Product> urunler = new List<Product>();
            foreach (var item in sec)
            {
                item.SelectSingleNode(".//img[@data-testid='productImage']").AddClass("foto");
                item.SelectSingleNode(".//a").AddClass("link");


            }
            foreach (var link in sec)
            {
                var isim = link.SelectSingleNode(".//img").Attributes["alt"].Value;
                var fiyat = link.SelectSingleNode(".//span[@class='buy-price']").InnerText.Trim();
                var foto = "";
                if (link.SelectSingleNode(".//img[@class='foto']").Attributes["src"].Value == "https://mcdn01.gittigidiyor.net/cdimg/anasayfa/nucleus/default-tn-30.jpg")
                {
                    foto = link.SelectSingleNode(".//img[@class='foto']").Attributes["data-src"].Value;
                }
                else
                {
                    foto = link.SelectSingleNode(".//img[@class='foto']").Attributes["src"].Value;
                }
                var urunlink = link.SelectSingleNode(".//a[@class='link']").Attributes["href"].Value;
                urunler.Add(new ggUrun()
                {
                    Price = double.Parse(fiyat.Trim('T', 'L', ' ')),
                    Name = isim,
                    PhotoUrl = foto,
                    Url = urunlink
                });

            }

            return urunler;
        }

        public List<Product> GetProductsByTeknosa(Request request)
        {
            var getHtml = _sendRequest.GetList(request);
            var sec = getHtml.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Contains("prd-inner")).ToList();
            List<Product> urunler = new List<Product>();

            foreach (var link in sec)
            {
                var isim = link.SelectSingleNode(".//img[@alt]").Attributes["alt"].Value;
                var fiyat = link.SelectSingleNode(".//div[@class='prd-prc2']").InnerText.Trim();
                var foto = link.SelectSingleNode(".//img[@alt]").Attributes["src"].Value;
                var urunlink = link.SelectSingleNode(".//input[@class='prd-compare-checkbox']").Attributes["data-compare-url"].Value;
                urunler.Add(new teknosaUrun()
                {
                    Price = double.Parse(fiyat.Trim('T', 'L', ' ')),
                    Name = isim,
                    PhotoUrl = foto,
                    Url = "https://www.teknosa.com" + urunlink

                });
            }

            return urunler;
        }
    }
}
