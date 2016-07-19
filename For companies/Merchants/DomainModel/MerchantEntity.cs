using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer
{
    public class Merchant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int MerchantProfit { get; set; }
        public int NumberOfOutlets { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateAdded { get; set; }
    }
}