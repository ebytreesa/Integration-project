using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceNinja.Models
{
    
    public class NinjaProductApiModel
    {
        public List<NinjaProduct> data { get; set; } = new List<NinjaProduct>();
        public NinjaProductMeta meta { get; set; }
    }

    public class NinjaProductMeta
    {
        public NinjaProductPagination pagination { get; set; }
    }

    public class NinjaProductPagination
    {
        public int total { get; set; }
        public int count { get; set; }
        public int per_page { get; set; }
        public int current_page { get; set; }
        public int total_pages { get; set; }
       // public object[] links { get; set; }
    }

    public class NinjaProductData
    {
        public NinjaProduct data { get; set; } = new NinjaProduct();
    }

    public class NinjaProduct
    {
        public string account_key { get; set; }
        public bool is_owner { get; set; }
        public int id { get; set; }
        public string product_key { get; set; }
        public string notes { get; set; }
        public float cost { get; set; }
        public int qty { get; set; }
        public string tax_name1 { get; set; }
        public int tax_rate1 { get; set; }
        public string tax_name2 { get; set; }
        public int tax_rate2 { get; set; }
        public int updated_at { get; set; }
        public object archived_at { get; set; }
        public string custom_value1 { get; set; }
        public string custom_value2 { get; set; }
        public bool is_deleted { get; set; }
    }

}
