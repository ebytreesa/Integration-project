using System;
using System.Collections.Generic;
using System.Text;

namespace Economic.Models
{

    public class EconPaymentTermsApiModel
    {
        public List<EconPaymentTerms> collection { get; set; } = new List<EconPaymentTerms>();
        public EconPaymentPagination pagination { get; set; } = new EconPaymentPagination();
        public string self { get; set; }
    }

    public class EconPaymentPagination
    {
        public int maxPageSizeAllowed { get; set; }
        public int skipPages { get; set; }
        public int pageSize { get; set; }
        public int results { get; set; }
        public int resultsWithoutFilter { get; set; }
        public string firstPage { get; set; }
        public string lastPage { get; set; }
    }

    public class EconPaymentTerms
    {
        public int paymentTermsNumber { get; set; }
        public int daysOfCredit { get; set; }
        public string name { get; set; }
        public string paymentTermsType { get; set; }
        public string self { get; set; }
    }
}
