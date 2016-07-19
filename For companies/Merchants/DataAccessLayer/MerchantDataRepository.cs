using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;


public class MerchantDataRepository : IMerchantDataRepository
{
    private IList<Merchant> merchants;
    public MerchantDataRepository()
    {
        merchants = new List<Merchant>()
        { 
            new Merchant() {Id = 1,Name = "Bank-E", AddedBy = "Joe bloggs", Country = "England", MerchantProfit = 10000, DateAdded = new DateTime(2016,01,01), NumberOfOutlets = 15},
            new Merchant(){Id = 2, Name = "Member-A", AddedBy = "Ray Brando", Country = "Italy", MerchantProfit = 243789, DateAdded = new DateTime(2016,01,11), NumberOfOutlets = 1300}
        };
    }

    public IEnumerable<Merchant> Create(Merchant merchant)
    {
        merchants.Add(merchant);
        return merchants;
    }

    public IEnumerable<Merchant> Delete(int id)
    {
        var merchantToBeDeleted = merchants.Where(merchant => merchant.Id == id).FirstOrDefault();
        merchants.Remove(merchantToBeDeleted);
        return merchants;
    }

    public IEnumerable<Merchant> Update(Merchant updatedMerchant)
    {

        var merchantToBeUpdated = merchants.Where(merchant => merchant.Id == updatedMerchant.Id).FirstOrDefault();
        if (merchantToBeUpdated != null)
        {
            merchantToBeUpdated = updatedMerchant;
        }
        return merchants;
    }

    public IEnumerable<Merchant> Get()
    {
        return merchants;
            
    }

    
}
