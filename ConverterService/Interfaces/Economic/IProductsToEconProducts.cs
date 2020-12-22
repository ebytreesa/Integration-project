using Domain.Models;
using Economic.Models;
using System.Collections.Generic;

namespace ConverterService.ProductConverter.Economic
{
    public interface IProductsToEconProducts
    {
        List<EconProduct> ToEconProducts(List<Product> products);
    }
}