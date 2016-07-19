using DataAccessLayer;
using Merchants.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Merchants.Controllers
{
    public class GlobalMerchantsApiController : ApiController
    {
        private IMerchantDataRepository repository;

        public GlobalMerchantsApiController(IMerchantDataRepository repository)
        {
            this.repository = repository;
        }

        public GlobalMerchantsApiController()
        {
            // TODO: Ideally it should be injected via DI Container.
            this.repository = new MerchantDataRepository();
        }

        [HttpGet]
        public HttpResponseMessage GetAllMerchants(HttpRequestMessage request)
        {
            IEnumerable<Merchant> result;
            result = repository.Get();


            if (result != null)
            {
                return request.CreateResponse(HttpStatusCode.OK, result);
            }

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }




        [HttpPost]
        public void AddMerchant(Merchant newMerchant)
        {
            IEnumerable<Merchant> result;
            result = repository.Create(newMerchant);
       }

        // PUT api/globalmerchantsapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/globalmerchantsapi/5
        public void Delete(int id)
        {
        }
    }
}
