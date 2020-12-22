using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using SQLDB.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLDB.Repositories
{
    public class CustomerContactRepository : Repository<CustomerContact>, ICustomerContactRepository
    {
        public CustomerContactRepository(CustomerDbContext context) : base(context)
        {

        }        

    }
}
