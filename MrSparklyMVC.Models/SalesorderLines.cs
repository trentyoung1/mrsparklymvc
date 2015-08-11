using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MrSparklyMVC.Models
{
    [MetadataType(typeof(SalesOrderLinesMetadata))]
    public partial class SalesOrderLine
    {
    }

    public class SalesOrderLinesMetadata
    {
        public int salesOrderLinesID { get; set; }
        public Nullable<int> salesOrderID { get; set; }
        public Nullable<int> productID { get; set; }
        [Display(Name="Quantity")]
        public Nullable<short> salesOrderItemQty { get; set; }
        [Display(Name="Price")]
        public Nullable<decimal> salesOrderItemPrice { get; set; }
        [Display(Name="Subtotal")]
        public Nullable<decimal> salesOrderLinesSubtotal { get; set; }
    }
}
