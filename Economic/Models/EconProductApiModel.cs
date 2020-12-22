using System;
using System.Collections.Generic;
using System.Text;

namespace Economic.Models
{


    public class EconProductsApiModel
    {
        public List<EconProduct> collection { get; set; } = new List<EconProduct>();
        public EconProductsPagination pagination { get; set; } = new EconProductsPagination();
        public EconProductsMetadata metaData { get; set; } = new EconProductsMetadata();
        public string self { get; set; }
    }

    public class EconProductsPagination
    {
        public int maxPageSizeAllowed { get; set; }
        public int skipPages { get; set; }
        public int pageSize { get; set; }
        public int results { get; set; }
        public int resultsWithoutFilter { get; set; }
        public string firstPage { get; set; }
        public string lastPage { get; set; }
    }

    public class EconProductsMetadata
    {
        public EconProductsCreate create { get; set; }
    }

    public class EconProductsCreate
    {
        public string description { get; set; }
        public string href { get; set; }
        public string httpMethod { get; set; }
    }

    public class EconProduct
    {
        public string productNumber { get; set; }
        public string name { get; set; }
        public float recommendedPrice { get; set; }
        public float salesPrice { get; set; }
        public bool barred { get; set; }
        public DateTime lastUpdated { get; set; }
        public EconProductgroup productGroup { get; set; } = new EconProductgroup();
        public EconProductsInvoices invoices { get; set; } = new EconProductsInvoices();
        public EconProductsPricing pricing { get; set; } = new EconProductsPricing();
        public string self { get; set; }
    }

    public class EconProductgroup
    {
        public int productGroupNumber { get; set; }
        public string name { get; set; }
        public string salesAccounts { get; set; }
        public string products { get; set; }
        public string self { get; set; }
    }
    
    public class EconProductsInvoices
    {
        public string drafts { get; set; }
        public string booked { get; set; }
        public string self { get; set; }
    }

    public class EconProductsPricing
    {
        public string currencySpecificSalesPrices { get; set; }
    }


}

