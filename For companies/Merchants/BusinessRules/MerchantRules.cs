using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessRules
{
    public class MerchantRules
    {

        private string errorMessages;

        public string ErrorMessages {
            get
            {
                return errorMessages;
            }
      
            private set
            {
                errorMessages = string.Empty;
            } 
        }

        public bool IsDuplicate(string merchantName, IList<Merchant> _existingMerchants)
        {
            if (_existingMerchants.Where(merchant => merchant.Name.ToLower() == merchantName.ToLower()).Count() > 0)
            {
                errorMessages += "Merchant Name already exists! ";
                return true;
            }
            return false;
        }

        public bool HasMetMinimumOutletRule(int numberOfOutlets)
        {
            bool isValid = true;
            if (numberOfOutlets < 10)
            {
                errorMessages += "Has Not Met MinimumOutletRule ";
                isValid = false;
            }
            return isValid;
        }

        public bool HasItMetEuropeanSpecificRule(string country, DateTime dateAdded)
        {
            bool isValid = true;
            if (country.ToLower().Equals("france"))
            {
                if ((dateAdded.Month == 1 && dateAdded.Day >= 10) || (dateAdded.Month == 2 && dateAdded.Day >= 24))  
                {
                    errorMessages += "Has Not Met France date rule";
                    isValid = false;
                }
            }
            else if (country.ToLower().Equals("spain"))
            {
                if ((dateAdded.Month == 2 && dateAdded.Day >= 15) 
                    || dateAdded.Month == 3 
                    || (dateAdded.Month == 4 && dateAdded.Day == 1))
                {
                    errorMessages += "Has Not Met Spain date rule";
                    isValid = false;
                }
            }
            return isValid;
        }

        public bool ValidateBusinessRules(Merchant modifiedMerchant, IList<Merchant> existingMerchants)
        {
            bool isDuplicate = IsDuplicate(modifiedMerchant.Name, existingMerchants);
            bool minOutletRule = HasMetMinimumOutletRule(modifiedMerchant.NumberOfOutlets);
            bool europeRule = HasItMetEuropeanSpecificRule(modifiedMerchant.Country, modifiedMerchant.DateAdded);
            return !isDuplicate && minOutletRule && europeRule;
        }
	
    }

    
}
