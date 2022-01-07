using System.Collections;
using System.Collections.Generic;

namespace Karsilastirma.Models
{
    public static class RequestParametersList
    {
        public static List<StaticListItem> VatanRequestTypes = new List<StaticListItem> {
            new StaticListItem {
                DocType = "div",
                ProductNameNodeType=".//div[@class='product-list__product-name']",
                ProductPriceNodeType=".//span[@class='product-list__price']",
                ProductImageNodeType=".//img[@class='lazyimg']",
                ProductUrlNodeType=".//a[@class='product-list__link']"
            } };
    }
}
