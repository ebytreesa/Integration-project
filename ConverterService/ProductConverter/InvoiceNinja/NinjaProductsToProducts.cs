using Domain.Models;
using InvoiceNinja.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConverterService.ProductConverter.InvoiceNinja
{
    public class NinjaProductsToProducts : INinjaProductsToProducts
    {
        public List<Product> ToProducts(List<NinjaProduct> ninjaProducts)
        {
            var products = new List<Product>();
            foreach (var item in ninjaProducts)
            {

                products.Add(new Product
                {
                    name = item.product_key,
                    ninjaProductId = item.id,
                    description = item.notes,
                    salesPrice = item.cost,
                    recommendedPrice = item.cost,
                    lastUpdated = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(item.updated_at),
                    dbUpdatedAt = DateTime.Now,
                    source = "Invoice Ninja",
                    sourceId = item.id.ToString()
                });
            }
            return products;
        }
    }
}
