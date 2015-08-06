using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MrSparklyMVC.Models
{
    [MetadataType(typeof(ProductsMetaData))]
    public partial class Product
    {
    }

    public class ProductsMetaData
    {
        [Display(Name= "Brand Name")]
        public string productBrandName { get; set; }
        [Display(Name="Cost Price")]
        public Nullable<decimal> productCostPrice { get; set; }
        [Display(Name="Retail Price")]
        public Nullable<decimal> productRetailPrice { get; set; }
        [Display(Name = "Qty")]
        public Nullable<short> productQty { get; set; }
    }
}
