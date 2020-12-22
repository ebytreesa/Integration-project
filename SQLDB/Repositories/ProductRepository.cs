using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using SQLDB.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLDB.Repositories
{
    public class ProductRepository :Repository<Product>, IProductRepository
    {
        public ProductRepository(CustomerDbContext context) : base(context)
        {

        }
    }
}
