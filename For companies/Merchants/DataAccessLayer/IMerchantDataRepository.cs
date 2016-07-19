using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer
{
    public interface IMerchantDataRepository
    {
        IEnumerable<Merchant> Create(Merchant merchant);

        IEnumerable<Merchant> Delete(int merchantId);

        IEnumerable<Merchant> Update(Merchant merchant);

        IEnumerable<Merchant> Get();
    }
}
