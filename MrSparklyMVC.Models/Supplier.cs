//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MrSparklyMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Supplier
    {
        public Supplier()
        {
            this.PurchaseOrders = new HashSet<PurchaseOrder>();
        }
    
        public int supplierID { get; set; }
        public string supplierName { get; set; }
        public string contactName { get; set; }
        public string supplierStreet { get; set; }
        public string supplierPhone { get; set; }
        public string supplierMobile { get; set; }
        public string supplierFax { get; set; }
        public string supplierEmail { get; set; }
        public Nullable<int> suburbID { get; set; }
    
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual Suburb Suburb { get; set; }
    }
}
