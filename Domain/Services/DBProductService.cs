using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class DBProductService : IDBProductService
    {
       
        private readonly IProductRepository _productRepository;
        public DBProductService(IProductRepository productRepository
            )
        {
            
            _productRepository = productRepository;
        }


        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _productRepository.GetAll();
        }

        public async Task CUDDBProducts(List<Product> products)
        {
            // Converting econProducts to products
           
            var dbProducts = await _productRepository.GetAll();

            foreach (var item in products)
            {
                var b = new Product();
                if (item.source == "Invoice Ninja")
                {
                    b = _productRepository.Search(x => x.ninjaProductId == item.ninjaProductId).Result.FirstOrDefault();

                }
                else if (item.source == "E-conomic")
                {
                    b = _productRepository.Search(x => x.econProductId == item.econProductId).Result.FirstOrDefault();
                }
                else
                {
                    throw new Exception("Unknown source");
                }

                #region 

                // update existing customer having the same customer source
                if (b != null)
                {
                    if (b.source == item.source)
                    {
                        item.productId = b.productId;
                        item.econProductId = b.econProductId;
                        item.ninjaProductId = b.ninjaProductId;
                        await Update(item);
                        //await _productRepository.Update(item);
                    }
                }
                else
                {
                    await Add(item);
                    //await _productRepository.Add(item);
                }
            }

            foreach (var item in dbProducts)
            {
                if (item.source == "Invoice Ninja")
                {
                    if (!products.Any(c => c.ninjaProductId == item.ninjaProductId))
                    {
                        //  await Remove(item);
                        item.ninjaProductId = 0;
                    }
                }

                if (item.source == "E-conomic")
                {
                    if (!products.Any(c => c.econProductId == item.econProductId))
                    {
                        //await Remove(item);
                        item.econProductId = " ";
                    }
                }

            }
            #endregion
        }

        public async Task Add(Product product)
        {
             await _productRepository.Add(product);
        }

        public async Task Update(Product product)
        {
            await _productRepository.Update(product);
        }

    }


}
