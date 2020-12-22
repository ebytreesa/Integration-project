using Domain.Models;
using Economic.Models;
using System.Collections.Generic;

namespace ConverterService.ProductConverter.Economic
{
    public interface IEconProductsToProducts
    {
        List<Product> ToProductList(List<EconProduct> econProducts);
    }
}