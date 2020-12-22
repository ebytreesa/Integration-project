using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        //private readonly ICustomerContactRepository _customerContactRepository;
        private readonly ICustomerContactService _customerContactService;
        private readonly IShippingAddressService _shippingAddressService;

        public CustomerService(ICustomerRepository customerRepository,
           // ICustomerContactRepository customerContactRepository,
            ICustomerContactService customerContactService,
            IShippingAddressService shippingAddressService)
        {
            _customerRepository = customerRepository;
            //_customerContactRepository = customerContactRepository;
            _customerContactService = customerContactService;
            _shippingAddressService = shippingAddressService;
        }

        public async Task CUD(List<Customer> customerList)
        {
            var source = customerList.FirstOrDefault().CustomerSource;
            var dbCustomers = await _customerRepository.GetAll();
            var sourceCus = dbCustomers.FindAll(x => x.CustomerSource == source).ToList();

            foreach (var item in customerList)
            {
                var b = new Customer();
                if (item.CustomerSource == "Invoice Ninja")
                {
                    b =  _customerRepository.SearchCustomer(x => x.InvoiceNinjaCustomerId == item.InvoiceNinjaCustomerId).Result;
                    
                }
                else if (item.CustomerSource == "E-conomic")
                {
                    b =  _customerRepository.SearchCustomer(x => x.EconomicCustomerId == item.EconomicCustomerId).Result;
                }
                else
                {
                    throw new Exception("Unknown source");
                }
                #region 

                // update existing customer having the same customer source
                if (b != null) 
                {
                    if (b.CustomerSource == item.CustomerSource)
                    {
                    
                        item.CustomerId = b.CustomerId;
                        item.EconomicCustomerId = b.EconomicCustomerId;
                        item.InvoiceNinjaCustomerId = b.InvoiceNinjaCustomerId;
                        await _customerRepository.Update(item);


                        //CUD customer Contacts
                        if (item.customerContacts.Count != 0)
                        {
                            await _customerContactService.CUDCustomerContacts(b, item.customerContacts);

                        }

                        //CUD shipping address
                        if (item.ShippingAddress.Count != 0)
                        {
                            await _shippingAddressService.CUDShippingAddress(b, item.ShippingAddress);
                        }
                    }
                }
                else 
                {
                    await _customerRepository.Add(item);
                }
            }


            #endregion

            //Delete customers from the same customer api source

            foreach (var item in dbCustomers)
            {
                if (item.CustomerSource == "Invoice Ninja")
                {
                    if (!customerList.Any(c => c.InvoiceNinjaCustomerId == item.InvoiceNinjaCustomerId))
                    {
                        //  await Remove(item);
                        item.InvoiceNinjaCustomerId = 0;
                    }
                }

                if (item.CustomerSource == "E-conomic")
                {
                    if (!customerList.Any(c => c.EconomicCustomerId == item.EconomicCustomerId))
                    {
                        //await Remove(item);
                        item.EconomicCustomerId = 0;
                    }
                }

            }
            

            //foreach (var dbItem in listToDelete)
            //{
            //    //var dbCus = await _customerRepository.SearchCustomer(x => x.CustomerId == sourceItem.CustomerId);

            //    if (dbItem.InvoiceNinjaCustomerId == dbItem.customerSourceId)
            //    {
            //        dbItem.InvoiceNinjaCustomerId = 0;
            //    }

            //    if (dbItem.EconomicCustomerId == dbItem.customerSourceId)
            //    {
            //        dbItem.EconomicCustomerId = 0;
            //    }

            //    if (dbItem.EconomicCustomerId == 0 && dbItem.InvoiceNinjaCustomerId == 0)
            //    {
            //      await  _customerRepository.Remove(dbItem);
            //    }

            //    await _customerRepository.UpdateSourceId(dbItem);
            //}

        }

        #region
        public async Task CUDCustomers(List<Customer> customerList)
        {

        //    //delete rows from db which is not in customerList
        //    //var dbList = _customerRepository.GetAll().Result;
        //    //foreach (var item in dbList)
        //    //{
        //    //    if (!customerList.Any(c => c.CustomerName == item.CustomerName && c.customerSourceId == item.customerSourceId))
        //    //    {
        //    //        foreach (var contact in item.customerContacts.ToList())
        //    //        {
        //    //            await _customerContactService.Remove(contact);
        //    //        }
        //    //        foreach(var address in item.ShippingAddress.ToList())
        //    //        {
        //    //            await _shippingAddressService.Remove(address);
        //    //        }
        //    //        await _customerRepository.Remove(item);
        //    //    }
        //    //}

        //    //Add or Update rows in db

        //    foreach (var cus in customerList)
        //    {
        //        //var existingCustomer =  _customerRepository.SearchCustomer(p => (p.customerSourceId == cus.customerSourceId
        //        //               && p.CustomerSource == cus.CustomerSource)).Result;
        //        var existingCustomer = _customerRepository.SearchCustomer(p => (p.CustomerName == cus.CustomerName)).Result;

        //        if (existingCustomer == null)
        //        {
        //            await _customerRepository.Add(cus);

        //        }
        //        else
        //        {
        //            // update existing customer
                    
        //            cus.CustomerId = existingCustomer.CustomerId;    //If there is no valid property key value then EF core will set the EntityState to Added.               
        //            await _customerRepository.Update(cus);

        //            //CUD customer Contacts
        //            if (cus.customerContacts.Count != 0)
        //            {                       
        //                await _customerContactService.CUDCustomerContacts(existingCustomer, cus.customerContacts);

        //            }

        //            //CUD shipping address
        //            if (cus.ShippingAddress.Count != 0)
        //            {
        //                await _shippingAddressService.CUDShippingAddress(existingCustomer, cus.ShippingAddress);
        //            }


        //        }
        //    }
        }
        #endregion
        public async Task<Customer> Add(Customer customer)
        {
            if (_customerRepository
                .Search(c => c.CustomerName == customer.CustomerName).Result.Any())
                return null;

            await _customerRepository.Add(customer);
            return customer;
        }

        

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _customerRepository.GetAll();
        }

        public async  Task<Customer> GetById(int id)
        {
            return await _customerRepository.GetById(id);
        }

        public async Task Remove(Customer customer)
        {
             await _customerRepository.Remove(customer);        }

        public async Task<Customer> Update(Customer customer)
        {
            if (_customerRepository.Search(c => c.CustomerName == customer.CustomerName && c.CustomerId != customer.CustomerId).Result.Any())
                return null;

            await _customerRepository.Update(customer);
            return customer;
        }
    }
}
//if (cus.customerContacts.Count != 0)
//{
//    cus.CustomerId = existingCustomer.CustomerId;
//    //Delete customer contact
//    var dbContactlist = await _customerContactRepository.Search(c => c.customer.CustomerId == cus.CustomerId);
//    foreach (var item in dbContactlist)
//    {
//        if (!cus.customerContacts.Any(c => c.customerContactNumber == item.customerContactNumber))
//        {
//           await  _customerContactRepository.Remove(item);
//        }

//    }                        

//    //update customer contacts
//    foreach (var item in cus.customerContacts)
//    {
//        var existingContact = existingCustomer.customerContacts
//            .Where(c => c.customerContactNumber == item.customerContactNumber)
//            .FirstOrDefault();

//        if (existingContact != null)
//        {
//            await _customerContactRepository.Update(item);
//        }
//        else
//        {
//            await _customerContactRepository.Add(item);
//        }
//    }
//}