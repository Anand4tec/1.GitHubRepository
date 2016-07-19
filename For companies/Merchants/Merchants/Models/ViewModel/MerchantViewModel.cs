using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Merchants.Models.ViewModel
{
    public class MerchantViewModel
    {
        
        public int Id { get; set; }

        [Required]
        [DisplayName("Merchant Name")]
        public string MerchantName { get; set; }

        [Required]
        [DisplayName("Country")]
        public string Country { get; set; }

        [Required]
        [DisplayName("Merchant Profit")]
        public int MerchantProfit { get; set; }

        [Required]
        [DisplayName("Number Of Outlets")]
        public int NumberOfOutlets { get; set; }

        [Required]
        [DisplayName("Added By")]
        public string AddedBy { get; set; }

        [Required]
        [DisplayName("Date Added")]
        public DateTime DateAdded { get; set; }
    }
}