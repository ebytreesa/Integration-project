using System;
using System.Collections.Generic;
using System.Text;

namespace Economic.Models
{
    public class EconCusContactApiModel
    {
        public List<EconCustomerContact> collection { get; set; } = new List<EconCustomerContact>();
        public EconCustomerContactPagination pagination { get; set; } = new EconCustomerContactPagination();
        public string self { get; set; }
    }

    public class EconCustomerContactPagination
    {
        public int maxPageSizeAllowed { get; set; }
        public int skipPages { get; set; }
        public int pageSize { get; set; }
        public int results { get; set; }
        public int resultsWithoutFilter { get; set; }
        public string firstPage { get; set; }
        public string lastPage { get; set; }
    }

    public class EconCustomerContact
    {
        public int customerContactNumber { get; set; }
        public string email { get; set; }
        public string eInvoiceId { get; set; }
        public string[] emailNotifications { get; set; } = new string[] { };
        public string name { get; set; }
        public string phone { get; set; }
        public BaseCustomer customer { get; set; } = new BaseCustomer();
       // public EconCustomer customer { get; set; } = new EconCustomer();
        public int sortKey { get; set; }
        public string self { get; set; }
        public bool isPrimary { get; set; }
    }

    public class BaseCustomer
    {
        public int customerNumber { get; set; }
        public string self { get; set; }
    }
}
