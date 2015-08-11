using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MrSparklyMVC.Models
{
    [MetadataType(typeof(RetailerMetadata))]
    public partial class Retailer
    {
    }

    public class RetailerMetadata
    {
        [Display(Name="Retailer")]
        public string retailerName { get; set; }
        [Display(Name="Contact")]
        public string retailerContactName { get; set; }
        [Display(Name="Street")]
        public string retailerStreet { get; set; }
        [Display(Name="Phone")]
        public string retailerPhone { get; set; }
        [Display(Name="Mobile")]
        public string retailerMob { get; set; }
        [Display(Name="Fax")]
        public string retailerFax { get; set; }
        [Display(Name="E-Mail")]
        public string retailerEmail { get; set; }
    }
}
