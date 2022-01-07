using HtmlAgilityPack;
using Karsilastirma.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Karsilastirma.Business
{
    public class SendRequest : ISendRequest
    {
        public HtmlDocument GetList(Request request)
        {
            HtmlDocument doc = new HtmlDocument();
            using (var client = new WebClient() { Encoding = Encoding.UTF8 })
            {
                client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36";
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                
                var istek = client.DownloadString(request.SiteUrl + request.Query);
                doc.LoadHtml(istek);
            }


            return doc;
        }
    }
}
