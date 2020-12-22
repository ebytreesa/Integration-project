using System;
using System.Collections.Generic;
using System.Text;

namespace Economic.Models
{

    public class EconCustomerApiModel
    {
        public List<EconCustomer> collection { get; set; } = new List<EconCustomer>();
        public EconPagination pagination { get; set; }
        // public Metadata metaData { get; set; }
        // public string self { get; set; }
    }

    public class EconPagination
    {
        public int maxPageSizeAllowed { get; set; }
        public int skipPages { get; set; }
        public int pageSize { get; set; }
        public int results { get; set; }
        public int resultsWithoutFilter { get; set; }
        public string firstPage { get; set; }
        public string lastPage { get; set; }
    }

    public class Metadata
    {
        public Create create { get; set; }
    }

    public class Create
    {
        public string description { get; set; }
        public string href { get; set; }
        public string httpMethod { get; set; }
    }

    public class EconCustomer 
    {
        public int customerNumber { get; set; }
        public string currency { get; set; }
        public EconPaymentTerms paymentTerms { get; set; }
        public EconCustomergroup customerGroup { get; set; }
        public string address { get; set; }
        public float balance { get; set; }
        public float dueAmount { get; set; }
        public string corporateIdentificationNumber { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string zip { get; set; }
        public string telephoneAndFaxNumber { get; set; }
        public string website { get; set; }
        public EconVatzone vatZone { get; set; } 
        public EconLayout layout { get; set; } 
        public DateTime lastUpdated { get; set; }
        public string contacts { get; set; }
        public EconTemplates templates { get; set; } 
        public EconTotals totals { get; set; } 
        public string deliveryLocations { get; set; }
        public EconInvoices invoices { get; set; } 
        public bool eInvoicingDisabledByDefault { get; set; }
        public string self { get; set; }
        public Attention attention { get; set; } 
        public float creditLimit { get; set; }
        public List<EconCustomerContact> econCustomerContacts { get; set; } = new List<EconCustomerContact>();
        public List<EconDeliveryLocation> econDeliveryLocations { get; set; } = new List<EconDeliveryLocation>();
    }

    //public class EconPaymentTerm
    //{
    //    public int paymentTermsNumber { get; set; }
    //    public string self { get; set; }
    //    public int daysOfCredit { get; set; }
    //    public string name { get; set; }
    //    public string paymentTermsType { get; set; }
    //}

    public class EconCustomergroup
    {
        public int customerGroupNumber { get; set; }
        public string self { get; set; }
    }

    public class EconVatzone
    {
        public int vatZoneNumber { get; set; }
        public string self { get; set; }
        public string name { get; set; }
    }

    public class EconLayout
    {
        public int layoutNumber { get; set; }
        public string self { get; set; }
    }

    public class EconTemplates
    {
        public string invoice { get; set; }
        public string invoiceLine { get; set; }
        public string self { get; set; }
    }

    public class EconTotals
    {
        public string drafts { get; set; }
        public string booked { get; set; }
        public string self { get; set; }
    }

    public class EconInvoices
    {
        public string drafts { get; set; }
        public string booked { get; set; }
        public string self { get; set; }
    }

    public class Attention
    {
        public int customerContactNumber { get; set; }
        public EconAttensionCustomer attentionCustomer { get; set; } // can it be EconCustomer?
        public EconCustomer customer { get; set; } = new EconCustomer();
        public string self { get; set; }
    }

    public class EconAttensionCustomer
    {
        public int customerNumber { get; set; }
        public string self { get; set; }
    }
}
