using BusinessRules;
using DataAccessLayer;
using Merchants.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Merchants.Controllers
{
    public class GlobalMerchantsController : Controller
    {
        //
        // GET: /GlobalMerchants/
        HttpClient Client = new HttpClient();

        public GlobalMerchantsController()
        {

        }

        public ActionResult Index(string searchTerm = null)
        {
            IEnumerable<MerchantViewModel> merchantViewModel = null;
            if (Session["merchants"] != null)
            {
                merchantViewModel = (IEnumerable<MerchantViewModel>)Session["merchants"];
            }
            else
            {
                Uri BaseAddress = new Uri("http://" + Request.Url.Authority);
                Client.BaseAddress = BaseAddress;


                var urlWithValue = "api/globalmerchantsapi/getallmerchants";
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    urlWithValue = urlWithValue + "?searchString=" + searchTerm;
                }

                HttpResponseMessage response = Client.GetAsync(urlWithValue).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsAsync<IEnumerable<Merchant>>().Result;
                    merchantViewModel = MapMerchantViewModel(data);
                    Session["merchants"] = merchantViewModel;
                }
            }

            if (Request.IsAjaxRequest())
            {
                var result = merchantViewModel.Where(merchant => merchant.Country.ToLower().Contains(searchTerm.ToLower()) ||
                                                    merchant.MerchantName.ToLower().Contains(searchTerm.ToLower()));

                return PartialView("_MerchantsList", result);
            }
            return View(merchantViewModel);

        }


        private IEnumerable<MerchantViewModel> MapMerchantViewModel(IEnumerable<Merchant> result)
        {
            IList<MerchantViewModel> merchantViewModel = new List<MerchantViewModel>();

            //Ideally this should be done in view model service using automapper.
            foreach (var entity in result)
            {
                var merchant = new MerchantViewModel()
                {
                    Id = entity.Id,
                    MerchantName = entity.Name,
                    Country = entity.Country,
                    MerchantProfit = entity.MerchantProfit,
                    NumberOfOutlets = entity.NumberOfOutlets,
                    AddedBy = entity.AddedBy,
                    DateAdded = entity.DateAdded
                };
                merchantViewModel.Add(merchant);
            }

            return merchantViewModel;
        }

        private Merchant MapAMerchant(MerchantViewModel merchantViewModel)
        {
            var merchant = new Merchant()
            {
                Id = merchantViewModel.Id,
                Name = merchantViewModel.MerchantName,
                Country = merchantViewModel.Country,
                MerchantProfit = merchantViewModel.MerchantProfit,
                NumberOfOutlets = merchantViewModel.NumberOfOutlets,
                AddedBy = merchantViewModel.AddedBy,
                DateAdded = merchantViewModel.DateAdded
            };
            return merchant;
        }

        private IList<Merchant> MapMerchants(IList<MerchantViewModel> merchantViewModelCollection)
        {
            IList<Merchant> merchants = new List<Merchant>();
            foreach (var merchantViewModel in merchantViewModelCollection)
            {
                var merchant = new Merchant()
                {
                    Id = merchantViewModel.Id,
                    Name = merchantViewModel.MerchantName,
                    Country = merchantViewModel.Country,
                    MerchantProfit = merchantViewModel.MerchantProfit,
                    NumberOfOutlets = merchantViewModel.NumberOfOutlets,
                    AddedBy = merchantViewModel.AddedBy,
                    DateAdded = merchantViewModel.DateAdded
                };
                merchants.Add(merchant);
            }     
            return merchants;
        }


        [HttpGet]
        public ActionResult CreateMerchant()
        {
            return View("AddMerchant");
        }

        [HttpPost]
        public ActionResult CreateMerchant(MerchantViewModel newMerchant)
        {
            IList<MerchantViewModel> merchantViewModel = null;
            if (Session["merchants"] != null)
                    {
                        merchantViewModel = (IList<MerchantViewModel>)Session["merchants"];
            }
            var businessRules = new MerchantRules();
            if (ModelState.IsValid && businessRules.ValidateBusinessRules(MapAMerchant(newMerchant), MapMerchants(merchantViewModel)))
            {
                Uri BaseAddress = new Uri("http://" + Request.Url.Authority);
                Client.BaseAddress = BaseAddress;
                var url = "api/globalmerchantsapi/AddMerchant";
                HttpResponseMessage response = Client.PostAsJsonAsync(url, MapAMerchant(newMerchant)).Result;

                if (response.IsSuccessStatusCode)
                {
                    if (merchantViewModel.Count > 0)
                    {
                        newMerchant.Id = merchantViewModel.Max(merchant => merchant.Id) + 1;
                    }
                    else
                    {
                        newMerchant.Id = 1;
                    }


                    merchantViewModel.Add(newMerchant);
                    Session["merchants"] = merchantViewModel;
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["BusinessError"] = businessRules.ErrorMessages;
            }
            return View("AddMerchant",newMerchant);
        }

        //
        // GET: /GlobalMerchants/Edit/5

        public ActionResult Edit(int id)
        {
            var merchantViewModel = (IList<MerchantViewModel>)Session["merchants"];
            var selectedMerchant = merchantViewModel.Single(merchant => merchant.Id == id);

            return View("EditMerchant", selectedMerchant);
        }

        //
        // POST: /GlobalMerchants/Edit/5

        [HttpPost]
        public ActionResult Edit(MerchantViewModel newMerchant)
        {
            try
            {
                //Ideally this logic should be implemented in DataRepository via WebAPI service(When SQL is available), due to interim persistence 
                // just imitating its behaviour using session here.
                var merchantViewModel = (IList<MerchantViewModel>)Session["merchants"];
                var selectedMerchant = merchantViewModel.Single(merchant => merchant.Id == newMerchant.Id);
                merchantViewModel.Remove(selectedMerchant);
                merchantViewModel.Add(newMerchant);
                Session["merchants"] = merchantViewModel;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /GlobalMerchants/Delete/5

        public ActionResult Delete(int id)
        {
            //Ideally this logic should be implemented in DataRepository via WebAPI service(When SQL is available), due to interim persistence 
            // just imitating its behaviour using session here.
            var merchantViewModel = (IList<MerchantViewModel>)Session["merchants"];
            var selectedMerchant = merchantViewModel.Single(merchant => merchant.Id == id);
            merchantViewModel.Remove(selectedMerchant);
            Session["merchants"] = merchantViewModel;
            return RedirectToAction("Index");
        }

    }
}