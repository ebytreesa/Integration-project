using Domain.Models;
using InvoiceNinja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConverterService
{
    public class NinjaCustomerToCustomer : INinjaCustomerToCustomer
    {
        public List<Customer> ToCustomerList(List<NinjaCustomerr> ninjaCustomers)
        {
            List<Customer> customerList = new List<Customer>();

            foreach (var item in ninjaCustomers)
            {
                customerList.Add(AsCustomer(item));
            }

            return customerList;
        }

        //Convert ninja customer to customer
        private static Customer AsCustomer(NinjaCustomerr ninjaCustomer)
        {
            List<CustomerContact> customerContacts = AsCustomerContacts(ninjaCustomer.contacts);
            customerContacts.Select(x => { x.customerNumber = (ninjaCustomer.id); return x; }).ToList();

            return new Customer()
            {
                CustomerSource = "Invoice Ninja",
                customerSourceId = ninjaCustomer.id,
                InvoiceNinjaCustomerId = ninjaCustomer.id,
                CustomerName = ninjaCustomer.name,
               
                // paymentTermsNumber
                //paymentTermsName
                paymentTermsDaysOfCredit = ninjaCustomer.payment_terms,
                //paymentTermsType
                address = ninjaCustomer.address1 + " " + ninjaCustomer.address2,
                balance = ninjaCustomer.balance,
                dueAmount = ninjaCustomer.balance - ninjaCustomer.paid_to_date,
                city = ninjaCustomer.city,
                state = ninjaCustomer.state,
                //country = 
                PostalCode = ninjaCustomer.postal_code,
                //VatNumber = Convert.ToInt32(ninjaCustomer.vat_number),
                //VatZoneName
                CorporateIdNumber = ninjaCustomer.vat_number,
                //currency
                //email 
                CustomerPhoneNumber = ninjaCustomer.work_phone,
                Website = ninjaCustomer.website,
                LastUpdated = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(ninjaCustomer.updated_at),
                Updated_at = DateTime.Now, //database updation

                ShippingAddress = new List<ShippingAddress>() { new ShippingAddress()
                {
                    address = ninjaCustomer.shipping_address1 + " " + ninjaCustomer.shipping_address2,
                    city = ninjaCustomer.city,
                    state = ninjaCustomer.state,
                    postalCode = ninjaCustomer.postal_code,
                    deliveryLocationNumber = 1,
                    updatedAt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(ninjaCustomer.updated_at),
                    customerNumber =ninjaCustomer.id,
                    //country =
                }
                },

                customerContacts = customerContacts

            };
        }

        //Convert ninjaCustomerContacts to customerContacts
        private static List<CustomerContact> AsCustomerContacts(List<NinjaCustomerContact> ninjaCustomerContacts)
        {
            List<CustomerContact> customerContacts = new List<CustomerContact>();
            foreach (var item in ninjaCustomerContacts)
            {
                customerContacts.Add(new CustomerContact()
                {
                    name = item.first_name + " " + item.last_name,
                    email = item.email,
                    phone = item.phone,
                    isPrimary = item.is_primary,
                    customerContactNumber = item.id,
                    updatedAt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(item.updated_at),
                    //customerNumber = 
                });
            }

            return customerContacts;
        }

    }
}
