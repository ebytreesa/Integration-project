using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IShippingAddressService
    {
        Task<IEnumerable<ShippingAddress>> GetAll();
        Task<ShippingAddress> GetById(int id);
        Task<ShippingAddress> Add(ShippingAddress shippingAddress);
        Task<ShippingAddress> Update(ShippingAddress shippingAddress);
        Task Remove(ShippingAddress shippingAddress);
        Task CUDShippingAddress(Customer cus, List<ShippingAddress> shippingAddresses);
    }
}
