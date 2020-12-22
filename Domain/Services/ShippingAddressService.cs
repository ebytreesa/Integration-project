using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ShippingAddressService : IShippingAddressService
    {
        public readonly IShippingAddressRepository _shippingAddressRepository;
        public ShippingAddressService(IShippingAddressRepository shippingAddressRepository)
        {
            _shippingAddressRepository = shippingAddressRepository;
        }
        public Task<ShippingAddress> Add(ShippingAddress shippingAddress)
        {
            throw new NotImplementedException();
        }

        public async Task CUDShippingAddress(Customer cus, List<ShippingAddress> shippingAddresses)
        {
            
                //Delete customer contact
                var dbShippingAddList = await _shippingAddressRepository.Search(c => c.customer.CustomerId == cus.CustomerId);
                foreach (var item in dbShippingAddList)
                {
                    if (!shippingAddresses.Any(c => c.deliveryLocationNumber == item.deliveryLocationNumber))
                    {
                        await Remove(item);
                    }

                }

                //add or update shipping address
                foreach (var item in shippingAddresses)
                {
                    var existingShippingAddress = cus.ShippingAddress
                        .Where(c => c.deliveryLocationNumber == item.deliveryLocationNumber)
                        .FirstOrDefault();

                    if (existingShippingAddress != null)
                    {
                        item.deliveryLocationId = existingShippingAddress.deliveryLocationId;
                        await _shippingAddressRepository.Update(item);
                    }
                    else
                    {
                        await _shippingAddressRepository.Add(item);
                    }
                }          


        }

        public Task<IEnumerable<ShippingAddress>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ShippingAddress> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Remove(ShippingAddress shippingAddress)
        {
            await _shippingAddressRepository.Remove(shippingAddress);
            
        }

        public Task<ShippingAddress> Update(ShippingAddress shippingAddress)
        {
            throw new NotImplementedException();
        }

        //Task IShippingAddressService.Remove(ShippingAddress shippingAddress)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
