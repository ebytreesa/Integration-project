//using Domain.Interfaces;
//using Domain.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Domain.Services
//{
//    public class NinjaToCusDbService
//    {
//        private readonly ICustomerRepository _customerRepository;
//        //private readonly ICustomerContactRepository _customerContactRepository;
//        private readonly ICustomerContactService _customerContactService;
//        private readonly IShippingAddressService _shippingAddressService;

//        public NinjaToCusDbService(ICustomerRepository customerRepository,
//            // ICustomerContactRepository customerContactRepository,
//            ICustomerContactService customerContactService,
//            IShippingAddressService shippingAddressService)
//        {
//            _customerRepository = customerRepository;
//            //_customerContactRepository = customerContactRepository;
//            _customerContactService = customerContactService;
//            _shippingAddressService = shippingAddressService;
//        }
//        public async Task CUD(List<Customer> customerList)
//        {
//            var source = customerList.FirstOrDefault().CustomerSource;
//            var dbCustomers = await _customerRepository.GetAll();
//            var sourceCus = new List<Customer>();
//            sourceCus = dbCustomers.FindAll(x => x.CustomerSource == source).ToList();

//            foreach (var item in customerList)
//            {
//                var b = await _customerRepository.SearchCustomer(x => x.InvoiceNinjaCustomerId == item.InvoiceNinjaCustomerId);
//                //var a = sourceCus.Where(x => x.InvoiceNinjaCustomerId == item.InvoiceNinjaCustomerId
//                //|| x.EconomicCustomerId == item.EconomicCustomerId).FirstOrDefault();
//                if (b != null)
//                {
//                    item.CustomerId = b.CustomerId;
//                    await _customerRepository.Update(item);
//                }
//                else
//                {
//                    await _customerRepository.Add(item);
//                }
//            }
//            #region
//            //var listToAdd = customerList.Where(a => sourceCus.All(b => b.customerSourceId != a.customerSourceId)).ToList();
//            //var listToUpdate = customerList.Where(a => sourceCus.Any(b => b.customerSourceId == a.customerSourceId)).ToList();
//            //foreach (var item in listToAdd)
//            //{
//            //    await Add(item);
//            //}

//            //foreach (var item in listToUpdate)
//            //{
//            //    var dbCus = sourceCus.Find(x => x.customerSourceId == item.customerSourceId);
//            //    item.CustomerId = dbCus.CustomerId;
//            //    await _customerRepository.Update(item);

//            //    //CUD customer Contacts
//            //    if (item.customerContacts.Count != 0)
//            //    {
//            //        await _customerContactService.CUDCustomerContacts(dbCus, item.customerContacts);

//            //    }

//            //    //CUD shipping address
//            //    if (item.ShippingAddress.Count != 0)
//            //    {
//            //        await _shippingAddressService.CUDShippingAddress(dbCus, item.ShippingAddress);
//            //    }
//            //}
//            #endregion
//            var listToDelete = sourceCus.Where(a => customerList.All(b => b.customerSourceId != a.customerSourceId)).ToList();


//            //foreach (var dbItem in listToDelete)
//            //{
//            //    //var dbCus = await _customerRepository.SearchCustomer(x => x.CustomerId == sourceItem.CustomerId);

//            //    if (dbItem.InvoiceNinjaCustomerId == dbItem.customerSourceId)
//            //    {
//            //        dbItem.InvoiceNinjaCustomerId = 0;
//            //    }

//            //    if (dbItem.EconomicCustomerId == dbItem.customerSourceId)
//            //    {
//            //        dbItem.EconomicCustomerId = 0;
//            //    }

//            //    if (dbItem.EconomicCustomerId == 0 && dbItem.InvoiceNinjaCustomerId == 0)
//            //    {
//            //      await  _customerRepository.Remove(dbItem);
//            //    }

//            //    await _customerRepository.UpdateSourceId(dbItem);
//            //}

//        }

//    }
//}
