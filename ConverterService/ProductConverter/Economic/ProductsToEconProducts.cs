using Domain.Models;
using Economic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConverterService.ProductConverter.Economic
{
    public class ProductsToEconProducts : IProductsToEconProducts
    {

        public List<EconProduct> ToEconProducts(List<Product> products)
        {
            var econProducts = new List<EconProduct>();
            foreach (var item in products)
            {
                if (item.econProductId != null)
                {
                    item.econProductId = item.econProductId;
                }
                else
                {
                    item.econProductId = "Ninja_"+item.ninjaProductId.ToString();
                }
                
                econProducts.Add(new EconProduct
                {                    
                    productNumber = item.econProductId,
                    name = item.name,
                    salesPrice = item.salesPrice,
                    recommendedPrice = item.recommendedPrice,
                    lastUpdated = item.lastUpdated,
                    productGroup = new EconProductgroup
                    {
                        productGroupNumber = 1,
                        name = "Varer m/moms",
                        salesAccounts = "https://restapi.e-conomic.com/product-groups/1/sales-accounts",
                        products = "https://restapi.e-conomic.com/product-groups/1/products",
                        self = "https://restapi.e-conomic.com/product-groups/1"
                    }
                });

            }
            return econProducts;
        }

    }
}
