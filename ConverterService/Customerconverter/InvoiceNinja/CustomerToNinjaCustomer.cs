using InvoiceNinja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace ConverterService.Customerconverter
{
    public class CustomerToNinjaCustomer : ICustomerToNinjaCustomer
    {
        //Convert customers to Ninja customers
        public  List<NinjaCustomerr> ToNinjaCustomerList(List<Customer> customers)
        {
            var ninjaCustomers = new List<NinjaCustomerr>();

           foreach (var customer in customers)
            {
                //List<NinjaCustomerContact> contacts = new List<NinjaCustomerContact>();

                ninjaCustomers.Add(new NinjaCustomerr()
                {
                    is_owner = true,
                    name = customer.CustomerName,
                    display_name = customer.CustomerName,
                    id = customer.InvoiceNinjaCustomerId,
                    balance = Convert.ToInt32(customer.balance),
                    // paid_to_date =
                    //updated_at =
                    //archived_at =
                    address1 = customer.address,
                    address2 = "",
                    city = customer.city,
                    state = customer.state,
                    postal_code = customer.PostalCode,
                    //country_id =

                    work_phone = customer.CustomerPhoneNumber,
                    //private_notes
                    //public_notes
                    //last_login
                    website = customer.Website,
                    //industry_id = int.Parse(customer.CorporateIdNumber),
                    //size_id
                    //is_deleted
                    //payment_terms = customer.paymentTerms,
                    vat_number = customer.VatNumber.ToString(),
                    vat_name = customer.VatZoneName,
                    id_number = customer.CustomerId.ToString(),
                    //language_id
                    //currency_id = 
                    //custom_value1
                    //custom_value2
                    //invoice_number_counter
                    //quote_number_counter
                    //task_rate

                    shipping_address1 = customer.ShippingAddress.Count != 0 ? customer.ShippingAddress[0].address : "",
                    ////shipping_address2 = customer.ShippingAddress==null ? "" : customer.ShippingAddress[0].address,
                    shipping_city = customer.ShippingAddress.Count != 0 ? customer.ShippingAddress[0].city : "",
                    shipping_state = customer.ShippingAddress.Count != 0 ? customer.ShippingAddress[0].state : "",
                    shipping_postal_code = customer.ShippingAddress.Count != 0 ? customer.ShippingAddress[0].postalCode : "",
                    ////shipping_country_id
                    //show_tasks_in_portal
                    //send_reminders
                    //credit_number_counter
                    //custom_messages
                    contacts = AsNinjaCustomerContacts(customer.customerContacts),

                });
            }
            return ninjaCustomers;
        }

        //Convert customerContacts to ninjacustomerContacts
        private static List<NinjaCustomerContact> AsNinjaCustomerContacts(List<CustomerContact> customerContacts)
        {
            List<NinjaCustomerContact> ninjaCustomerContacts = new List<NinjaCustomerContact>();
            foreach (var item in customerContacts)
            {
                ninjaCustomerContacts.Add(new NinjaCustomerContact()
                {
                    first_name = (item.name).Split(' ').First(),
                    last_name = (item.name).Split(' ').Last(),
                    phone = item.phone,
                    email = item.email,
                    //id = item.ninjaCustomerContactNumber,
                    is_primary = item.isPrimary
                });
            }
            return ninjaCustomerContacts;
        }
    }
}
