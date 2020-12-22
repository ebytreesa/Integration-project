//using ConverterService.Models;
using Economic.Models;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConverterService.Customerconverter.Economic
{
    public class CustomerToEconomicCustomer : ICustomerToEconomicCustomer
    {
        //Convert customer to e-conomic customer
        public static EconCustomer AsEconCustomer(Customer cus)
        {

            return new EconCustomer()
            {
                name = cus.CustomerName,
                //customerNumber = Convert.ToInt32(cus.CustomerId),
                //customerNumber = cus.EconomicCustomerId,
                currency = cus.currency == null ? "DKK" : cus.currency,
                paymentTerms = new EconPaymentTerms()
                {
                    name = cus.paymentTermsName,
                    //paymentTermsNumber = cus.paymentTermsNumber,
                    paymentTermsType = cus.paymentTermsType,
                    daysOfCredit = cus.paymentTermsDaysOfCredit,
                    // self = "https://restapi.e-conomic.com/payment-terms/"+ cus.paymentTermsDaysOfCredit
                },
                customerGroup = new EconCustomergroup()
                {
                    customerGroupNumber = 1, // default value
                    self = "https://restapi.e-conomic.com/customer-group/1"

                },
                address = cus.address,
                balance = cus.balance,
                dueAmount = cus.dueAmount,
                corporateIdentificationNumber = cus.CorporateIdNumber,
                city = cus.city,
                country = cus.country == null ? " " : cus.country,
                email = cus.email == null ? " " : cus.email,
                zip = cus.PostalCode,
                telephoneAndFaxNumber = cus.CustomerPhoneNumber,
                website = cus.Website,
                vatZone = new EconVatzone()
                {
                    vatZoneNumber = cus.vatZoneNumber == 0 ? 1 : cus.vatZoneNumber,
                    self = "https://restapi.e-conomic.com/vat-zones/" + (cus.vatZoneNumber == 0 ? 1 : cus.vatZoneNumber)
                    //name = cus.VatZoneName
                },

                layout = new EconLayout()
                {

                    layoutNumber = 20, // default value
                    self = "https://restapi.e-conomic.com/layouts/20"
                },
                lastUpdated = cus.Updated_at,

                // Api will create contacts and deliveryLocation url
                // contacts = "https://restapi.e-conomic.com/customers/"+cus.CustomerId+"/contacts",
                //deliveryLocations = "https://restapi.e-conomic.com/customers/" + cus.CustomerId + "/delivery-locations",
                //"Attention cannot be set when creating a customer."
                //attention = new Attention() { },
                econCustomerContacts = AsEconcustomerContacts(cus.customerContacts),
                econDeliveryLocations = AsEcondeliverylocations(cus.ShippingAddress)

            };
        }


        //Convert customers to e-conomic customers
        public List<EconCustomer> ToEconCustomerList(List<Customer> customers)
        {
            List<EconCustomer> econCustomers = new List<EconCustomer>();
            foreach (var item in customers)
            {
                econCustomers.Add(AsEconCustomer(item));
            }
            return econCustomers;
        }

        //Convert  customerContacts to e-conomic customerContacts
        private static List<EconCustomerContact> AsEconcustomerContacts(List<CustomerContact> customerContacts)
        {
            List<EconCustomerContact> econCustomerContacts = new List<EconCustomerContact>();
            foreach (var item in customerContacts)
            {
                econCustomerContacts.Add(new EconCustomerContact()
                {
                    name = item.name,
                    email = item.email,
                    // E-conomic Api will create customerContactNumber
                    //customerContactNumber = item.customerContactNumber,
                    eInvoiceId = " ",
                    emailNotifications = new string[] { },                    
                    isPrimary = item.isPrimary,
                    //customer = new EconCustomer()
                    customer = new BaseCustomer()
                    {
                        customerNumber = item.customerNumber,
                        self = "https://restapi.e-conomic.com/customers/" + item.customer.EconomicCustomerId,
                    },
                    phone = item.phone,
                    self = "https://restapi.e-conomic.com/customers/contacts/" + item.customerNumber + "/contacts/",

                });
            }
            return econCustomerContacts;
        }

        //Convert customer shipping address to e-conomic delivery address

        private static List<EconDeliveryLocation> AsEcondeliverylocations(List<ShippingAddress> shippingAddresses)
        {
            List<EconDeliveryLocation> econDeliveryLocations = new List<EconDeliveryLocation>();
            foreach (var item in shippingAddresses)
            {
                econDeliveryLocations.Add(new EconDeliveryLocation()
                {
                    //deliveryLocationNumber = item.deliveryLocationNumber,
                    address = item.address,
                    postalCode = item.postalCode,
                    city = item.city,
                    country = item.country == null ? " " : item.country,
                    termsOfDelivery = item.termsOfDelivery == null ? " " : item.termsOfDelivery,
                    sortKey = 1, // default value
                    customer = new EconDeliveryCustomer()
                    {
                        customerNumber = item.customerNumber,
                        self = "https://restapi.e-conomic.com/customers/" + item.customerNumber,
                    },
                    self = "https://restapi.e-conomic.com/customers/" + item.customerNumber + "/delivery-locations"

                });
            }
            return econDeliveryLocations;
        }
    }
}
