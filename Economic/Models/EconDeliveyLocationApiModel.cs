using System;
using System.Collections.Generic;
using System.Text;

namespace Economic.Models
{


    public class EconDeliveryLocationApiModel
    {
        public List<EconDeliveryLocation> collection { get; set; } = new List<EconDeliveryLocation>();
        public EconDeliveryPagination pagination { get; set; } = new EconDeliveryPagination();
        public string self { get; set; }
    }

    public class EconDeliveryPagination
    {
        public int maxPageSizeAllowed { get; set; }
        public int skipPages { get; set; }
        public int pageSize { get; set; }
        public int results { get; set; }
        public int resultsWithoutFilter { get; set; }
        public string firstPage { get; set; }
        public string lastPage { get; set; }
    }

    public class EconDeliveryLocation
    {
        public int deliveryLocationNumber { get; set; }
        public string address { get; set; }
        public string postalCode { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string termsOfDelivery { get; set; }
        public int sortKey { get; set; }
        public EconDeliveryCustomer customer { get; set; } = new EconDeliveryCustomer();
        public bool barred { get; set; }
        public string self { get; set; }
    }

    public class EconDeliveryCustomer
    {
        public int customerNumber { get; set; }
        public string self { get; set; }
    }
}
