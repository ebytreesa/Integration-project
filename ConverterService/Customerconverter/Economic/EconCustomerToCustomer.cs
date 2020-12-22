using Domain.Models;
using Economic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConverterService.Customerconverter.Economic
{
    public class EconCustomerToCustomer : IEconCustomerToCustomer
    {
        public List<Customer> ToCustomerList(List<EconCustomer> ecustomers)
        {
            List<Customer> customerList = new List<Customer>();


            foreach (var ECustomer in ecustomers)
            {
                Customer c = AsCustomer(ECustomer);
                customerList.Add(c);
            }


            return customerList;

        }

        //Convert e-conomic customer to customer
        private static Customer AsCustomer(EconCustomer econCustomer)
        {

            List<CustomerContact> contacts = new List<CustomerContact>();
            contacts = AsCustomerContacts(econCustomer.econCustomerContacts);
            //CustomerContact c = contacts.Find(x => x.customerContactNumber == econCustomer.attention.customerContactNumber);
            //if (c != null) 
            //    c.isPrimary = true;


            return new Customer()
            {
                CustomerSource = "E-conomic",
                CustomerName = econCustomer.name,
                EconomicCustomerId = econCustomer.customerNumber,
                //InvoiceNinjaCustomerId = 0,
                customerSourceId = econCustomer.customerNumber,
                //CustomerId = AddSourceInfo(econCustomer.customerNumber.ToString()),
                paymentTermsNumber = econCustomer.paymentTerms.paymentTermsNumber,
                paymentTermsDaysOfCredit = econCustomer.paymentTerms.daysOfCredit,
                paymentTermsName = econCustomer.paymentTerms.name,
                paymentTermsType = econCustomer.paymentTerms.paymentTermsType,
                address = econCustomer.address,
                balance = econCustomer.balance,
                city = econCustomer.city,
                country = econCustomer.country,
                PostalCode = econCustomer.zip,
                dueAmount = econCustomer.dueAmount,
               // VatNumber = Convert.ToInt32(econCustomer.corporateIdentificationNumber),
                VatZoneName = econCustomer.vatZone.name,
                CorporateIdNumber = econCustomer.corporateIdentificationNumber,
                email = econCustomer.email,
                currency = econCustomer.currency,
                CustomerPhoneNumber = econCustomer.telephoneAndFaxNumber,
                Website = econCustomer.website,
                LastUpdated = econCustomer.lastUpdated,
                Updated_at = DateTime.Now,
                ShippingAddress = AsCustomerShippingAddress(econCustomer.econDeliveryLocations),
                customerContacts = contacts,
            };
        }
        //convertr e-conomic CustomerContacts to customerContacts
        public static List<CustomerContact> AsCustomerContacts(List<EconCustomerContact> econCustomerContacts)
        {
            List<CustomerContact> customerContacts = new List<CustomerContact>();
            foreach (var item in econCustomerContacts)
            {
                CustomerContact customerContact = new CustomerContact();
                customerContact.customerContactNumber = item.customerContactNumber;
                customerContact.email = item.email;
                customerContact.phone = item.phone;
                customerContact.name = item.name;
                customerContact.customerNumber = item.customer.customerNumber;

                customerContacts.Add(customerContact);
            }
            return customerContacts;
        }

        //convertr e-conomic CustomerContacts to customerContacts
        public static List<ShippingAddress> AsCustomerShippingAddress(List<EconDeliveryLocation> econDeliveryLocations)
        {
            List<ShippingAddress> customerShippingAddress = new List<ShippingAddress>();
            foreach (var item in econDeliveryLocations)
            {
                ShippingAddress shippingAddress = new ShippingAddress();
                shippingAddress.address = item.address;
                shippingAddress.city = item.city;
                shippingAddress.termsOfDelivery = item.termsOfDelivery;
                shippingAddress.country = item.country;
                shippingAddress.customerNumber = item.customer.customerNumber;
                shippingAddress.deliveryLocationNumber = item.deliveryLocationNumber;
                shippingAddress.postalCode = item.postalCode;
                customerShippingAddress.Add(shippingAddress);
            }
            return customerShippingAddress;
        }
    }
}
