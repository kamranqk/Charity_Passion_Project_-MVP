using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using PassionProjectChariity_s2022.Models;
using PassionProjectChariity_s2022.Models.ViewModels;
using System.Web.Script.Serialization;

namespace PassionProjectChariity_s2022.Controllers
{
    public class FundController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static FundController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44382/api/");
        }

        // GET: Fund/List
        public ActionResult List()
        {
            //objective: communicate with our Fund data api to retrieve a list of funds
            //curl https://localhost:44382/api/Funddata/listFunds


            string url = "funddata/listfunds";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<FundDto> funds = response.Content.ReadAsAsync<IEnumerable<FundDto>>().Result;
            //Debug.WriteLine("Number of Funds received : ");
            //Debug.WriteLine(Funds.Count());


            return View(funds);
        }

        // GET: Fund/Details/5
        public ActionResult Details(int id)    
        {
            
            //objective: communicate with our Fund data api to retrieve one Fund
            //curl https://localhost:44324/api/Funddata/findfund/{id}

            DetailsFund ViewModel = new DetailsFund();

            string url = "funddata/findfund/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            FundDto SelectedFund = response.Content.ReadAsAsync<FundDto>().Result;
            Debug.WriteLine("Fund received : ");
            Debug.WriteLine(SelectedFund.FundName);

            ViewModel.SelectedFund = SelectedFund;

            //showcase information about Donation related to this fund
            //send a request to gather information about donations related to a particular fund ID
            url = "donationdata/listdonationsforfund/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<DonationDto> RelatedDonations = response.Content.ReadAsAsync<IEnumerable<DonationDto>>().Result;

            ViewModel.RelatedDonations = RelatedDonations;

            
            return View(ViewModel);
        }
        public ActionResult Error()
        {

            return View();
        }

        // GET: Fund/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Fund/Create
        [HttpPost]
        public ActionResult Create(Fund Fund)
        {
            Debug.WriteLine("the json payload is :");
            Debug.WriteLine(Fund.FundName);
            //objective: add a new Fund into our system using the API
            //curl -H "Content-Type:application/json" -d @Fund.json https://localhost:44382/api/funddata/addfund
            string url = "funddata/addfund";


            string jsonpayload = jss.Serialize(Fund);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }


        // GET: Fund/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "funddata/findfund/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            FundDto selectedfund = response.Content.ReadAsAsync<FundDto>().Result;
            return View(selectedfund);
        }

        // POST: Fund/Update/5
        [HttpPost]
        public ActionResult Update(int id, Fund Fund)
        {
            string url = "funddata/updatefund/" + id;
            string jsonpayload = jss.Serialize(Fund);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Fund/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "funddata/findfund/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            FundDto selectedFund = response.Content.ReadAsAsync<FundDto>().Result;
            return View(selectedFund);
        }

        // POST: Fund/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "funddata/deletefund/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
