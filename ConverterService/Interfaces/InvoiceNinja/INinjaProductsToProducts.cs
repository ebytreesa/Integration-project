using Domain.Models;
using InvoiceNinja.Models;
using System.Collections.Generic;

namespace ConverterService.ProductConverter.InvoiceNinja
{
    public interface INinjaProductsToProducts
    {
        List<Product> ToProducts(List<NinjaProduct> ninjaProducts);
    }
}