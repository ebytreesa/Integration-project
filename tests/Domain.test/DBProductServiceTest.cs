using Domain.Interfaces;
using Domain.Models;
using Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Domain.test
{
    public class DBProductServiceTest
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly DBProductService _dBProductService;

        public DBProductServiceTest()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _dBProductService = new DBProductService(_productRepositoryMock.Object);
        }

        [Fact]
        public async void GetAll_ReturnProductList_whenProductExists()
        {
            var products = CreateProductList();
            _productRepositoryMock.Setup(p => p.GetAll()).ReturnsAsync(products);

            var result = await _dBProductService.GetAll();
            Assert.NotNull(result);
            Assert.IsType<List<Product>>(result);
        }

        [Fact]
        public async void GetAll_ReturnProductList_whenProductDoesNotExists()
        {
            _productRepositoryMock.Setup(p => p.GetAll()).ReturnsAsync((List<Product>)null);

            var result = await _dBProductService.GetAll();
            Assert.Null(result);
        }


        private List<Product> CreateProductList()
        {
            return new List<Product>()
            {
                new Product()
                {
                    name = "Test product 1",
                    salesPrice = 12,
                    productId = 2

                },
                new Product()
                {
                    name = "Test product 2",
                    salesPrice = 12,
                    productId = 3

                },
                new Product()
                {
                    name = "Test product 3",
                    salesPrice = 13,
                    productId = 4

                }

            };

        }
    }
}
