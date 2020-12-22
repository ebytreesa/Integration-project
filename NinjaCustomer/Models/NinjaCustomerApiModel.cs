using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceNinja.Models
{
   
        public class NinjaCustomerApiModel
        {
            public List<NinjaCustomerr> data { get; set; } = new List<NinjaCustomerr>();
            public Meta meta { get; set; }
        }

        public class Meta
        {
            public NinjaPagination pagination { get; set; }
        }

        public class NinjaPagination
        {
            public int total { get; set; }
            public int count { get; set; }
            public int per_page { get; set; }
            public int current_page { get; set; }
            public int total_pages { get; set; }
           // public Links links { get; set; }
        }

    //public class Links
    //{
    //    public string next { get; set; }
    //}

    public class NinjaCusData
    {
        public NinjaCustomerr data { get; set; }
    }


    public class NinjaCustomerr 
        {
            public string account_key { get; set; }
            public bool is_owner { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public string display_name { get; set; }
            public int balance { get; set; }
            public int paid_to_date { get; set; }
            public int updated_at { get; set; }
            public int? archived_at { get; set; }
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string postal_code { get; set; }
            public int country_id { get; set; }
            public string work_phone { get; set; }
            public string private_notes { get; set; }
            public string public_notes { get; set; }
            public string last_login { get; set; }
            public string website { get; set; }
            public int industry_id { get; set; }
            public int size_id { get; set; }
            public bool is_deleted { get; set; }
            public int payment_terms { get; set; }
            public string vat_number { get; set; }
            public string vat_name { get; set; }
            public string id_number { get; set; }
            public int language_id { get; set; }
            public int currency_id { get; set; }
            public string custom_value1 { get; set; }
            public string custom_value2 { get; set; }
            public int invoice_number_counter { get; set; }
            public int quote_number_counter { get; set; }
            public int task_rate { get; set; }
            public string shipping_address1 { get; set; }
            public string shipping_address2 { get; set; }
            public string shipping_city { get; set; }
            public string shipping_state { get; set; }
            public string shipping_postal_code { get; set; }
            public int shipping_country_id { get; set; }
            public bool show_tasks_in_portal { get; set; }
            public bool send_reminders { get; set; }
            public int credit_number_counter { get; set; }
           // public object custom_messages { get; set; } 
        public List<NinjaCustomerContact> contacts { get; set; } = new List<NinjaCustomerContact>();
        }

        public class NinjaCustomerContact
        {
            public string account_key { get; set; }
            public bool is_owner { get; set; }
            public int id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string email { get; set; }
            public string contact_key { get; set; }
            public int updated_at { get; set; }
            public object archived_at { get; set; }
            public bool is_primary { get; set; }
            public string phone { get; set; }
            public string last_login { get; set; }
            public bool send_invoice { get; set; }
            public string custom_value1 { get; set; }
            public string custom_value2 { get; set; }
        }
    }

