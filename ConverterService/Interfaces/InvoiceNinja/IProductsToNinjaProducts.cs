using Domain.Models;
using InvoiceNinja.Models;
using System.Collections.Generic;

namespace ConverterService.ProductConverter.InvoiceNinja
{
    public interface IProductsToNinjaProducts
    {
        List<NinjaProduct> ToNinjaProducts(List<Product> products);
    }
}