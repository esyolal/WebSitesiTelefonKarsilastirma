using HtmlAgilityPack;
using Karsilastirma.Models;
using System.Collections;
using System.Collections.Generic;

namespace Karsilastirma.Business
{
    public interface ISendRequest
    {
        HtmlDocument GetList(Request request);
    }
}
