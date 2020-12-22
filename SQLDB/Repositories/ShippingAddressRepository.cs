using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using SQLDB.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLDB.Repositories
{
    public class ShippingAddressRepository : Repository<ShippingAddress>, IShippingAddressRepository
    {
        public ShippingAddressRepository(CustomerDbContext context) : base(context)
        {

        }

    }
}
