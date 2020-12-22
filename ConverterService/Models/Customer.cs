//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Text;

//namespace ConverterService.Models
//{
//    public class Customer
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int CustomerId { get; set; }
//        public string CustomerSource { get; set; }
//        public int customerSourceId { get; set; }
//        public int EconomicCustomerId { get; set; }
//        public int InvoiceNinjaCustomerId { get; set; }
//        public string CustomerName { get; set; }
//        public DateTime Updated_at { get; set; }
//        public DateTime Created_at { get; set; }
//        public int paymentTermsNumber { get; set; }
//        public string paymentTermsName { get; set; }
//        public int paymentTermsDaysOfCredit { get; set; }
//        public string paymentTermsType { get; set; }
//        public string address { get; set; }
//        public float balance { get; set; }
//        public float dueAmount { get; set; }
//        public string city { get; set; }
//        public string state { get; set; }
//        public string country { get; set; }
//        public string PostalCode { get; set; }
//        public int VatNumber { get; set; } //make int to string
//        public int vatZoneNumber { get; set; }
//        public string VatZoneName { get; set; }
//        public string CorporateIdNumber { get; set; }
//        public string currency { get; set; }
//        public string email { get; set; }
//        public string CustomerPhoneNumber { get; set; }
//        public string Website { get; set; }
//        public DateTime LastUpdated { get; set; }

//        public List<ShippingAddress> ShippingAddress { get; set; } = new List<ShippingAddress>();
//        public List<CustomerContact> customerContacts { get; set; } = new List<CustomerContact>();

//    }

//    public class CustomerContact
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int customerContactId { get; set; }
//        public int customerContactNumber { get; set; }//customerContact.id
//        public string email { get; set; }
//        public string name { get; set; }
//        public string phone { get; set; }
//        public bool isPrimary { get; set; }
//        public DateTime updatedAt { get; set; }

//        public int customerNumber { get; set; }//customer.customerId
//        public Customer customer { get; set; }

//    }

//    public class ShippingAddress
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int deliveryLocationId { get; set; }
//        public int deliveryLocationNumber { get; set; }
//        public string address { get; set; }
//        public string postalCode { get; set; }
//        public string city { get; set; }
//        public string state { get; set; }
//        public string country { get; set; }
//        public string termsOfDelivery { get; set; }
//        public string self { get; set; }
//        public DateTime updatedAt { get; set; }


//        public int customerNumber { get; set; }
//        public Customer customer { get; set; }

//    }
//}
