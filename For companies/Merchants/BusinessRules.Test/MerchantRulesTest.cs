using DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessRules.Test
{
    [TestClass()]
    public class MerchantRulesTest
    {
        [TestMethod]
        public void Should_Not_Allow_Duplicate_Merchant_Names()
        {
            var rules = new MerchantRules();

            var merchantsList = new List<Merchant>();

            bool isDuplicate = rules.IsDuplicate("Merchant1",merchantsList);
            Assert.AreEqual(isDuplicate, false,"Duplicate merchant name identified!");

            merchantsList.Add(new Merchant() { Name = "Merchant1" });
            isDuplicate = rules.IsDuplicate("Merchant1", merchantsList);
            Assert.AreEqual(isDuplicate, true, "Duplicate merchant rule failed!");
        }

        [TestMethod]
        public void Should_Not_Allow_Merchants_With_Less_Number_Of_Outlets()
        {
            var rules = new MerchantRules();

            bool hasMinNoOfOutlets = rules.HasMetMinimumOutletRule(11);
            Assert.AreEqual(hasMinNoOfOutlets, true, "Minimum number of Outlets rule failed!");

            hasMinNoOfOutlets = rules.HasMetMinimumOutletRule(5);
            Assert.AreEqual(hasMinNoOfOutlets, false, "Minimum number of Outlets rule failed!");
        }

        [TestMethod]
        public void Should_Adhere_To_European_Rules()
        {
            var rules = new MerchantRules();

            bool hasMetFranceRule = rules.HasItMetEuropeanSpecificRule("France", new DateTime(2016,01,10));
            Assert.AreEqual(hasMetFranceRule, false, "Date should not be in Jan 10!");

            hasMetFranceRule = rules.HasItMetEuropeanSpecificRule("France", new DateTime(2016, 01, 9));
            Assert.AreEqual(hasMetFranceRule, true, "Rule should have allowed date Jan 9 for france!");


            bool hasMetSpainRule = rules.HasItMetEuropeanSpecificRule("Spain", new DateTime(2016, 03, 10));
            Assert.AreEqual(hasMetSpainRule, false, "Date should not be in March for Spain!");

            hasMetSpainRule = rules.HasItMetEuropeanSpecificRule("Spain", new DateTime(2016, 01, 9));
            Assert.AreEqual(hasMetSpainRule, true, "Rule should have allowed date Jan 9 for spain!");
        }
    }
}
