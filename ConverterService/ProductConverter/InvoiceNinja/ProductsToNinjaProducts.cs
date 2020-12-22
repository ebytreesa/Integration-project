using Domain.Models;
using InvoiceNinja.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConverterService.ProductConverter.InvoiceNinja
{
    public class ProductsToNinjaProducts : IProductsToNinjaProducts
    {
        public List<NinjaProduct> ToNinjaProducts(List<Product> products)
        {
            var ninjaProducts = new List<NinjaProduct>();
            foreach (var item in products)
            {
                ninjaProducts.Add(new NinjaProduct
                {
                    product_key = item.name,
                    cost = (int)item.salesPrice,
                    qty = 1,
                    notes = item.description,

                });
            }
            return ninjaProducts;
        }
    }
}
