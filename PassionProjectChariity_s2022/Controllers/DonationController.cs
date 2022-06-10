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
    public class DonationController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static DonationController()
        { 
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44382/api/");
        }

        // GET: Donation/List
        public ActionResult List()
        {
            //objective: Communicate with our donation data api to retreive a list of donations.
            //curl https://localhost:44382/api/donationdata/listdonations
             
            string url = "donationdata/listdonations";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is "); 
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<DonationDto> donations = response.Content.ReadAsAsync<IEnumerable<DonationDto>>().Result;
            //Debug.WriteLine("Number of animals received : ");
            //Debug.WriteLine(donations.Count());

            return View(donations);
        }

        // GET: Donation/Details/5
        public ActionResult Details(int id)
        {
            DetailsDonation ViewModel = new DetailsDonation();

            //objective: Communicate with our donation data api to retreive one donation 
            //curl https://localhost:44382/api/donationdata/finddonation/{id}

            string url = "donationdata/finddonation/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            DonationDto SelectedDonation = response.Content.ReadAsAsync<DonationDto>().Result;
            //Debug.WriteLine("Donation received : ");
            //Debug.WriteLine("selecteddonation.DonationAmount");

            ViewModel.SelectedDonation = SelectedDonation;

            return View(ViewModel);
        }
        public ActionResult Error()
        {
            return View();        
        }

        // GET: Donation/New
        public ActionResult New()
        {
            //information about all funds in the system
            //GET api/funddata/listfunds

            string url = "funddata/listfunds";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<FundDto> FundOptions = response.Content.ReadAsAsync<IEnumerable<FundDto>>().Result;

            return View(FundOptions);
        }

        // POST: Donation/Create
        [HttpPost]
        public ActionResult Create(Donation donation)
        {
            Debug.WriteLine("The json payload is :");
            Debug.WriteLine(donation.DonationAmount);
            //objective: add a new donation into our system using the API 
            //curl -H "Content-Type:application/json" https://localhost:44382/api/donationdata/adddonation
            string url = "donationdata/adddonation";

            
            string jsonpayload = jss.Serialize(donation);

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

        // GET: Donation/Edit/5
        public ActionResult Edit(int id)
        {
            //objective: Communicate with our donation data api to Edit donation 
            //curl https://localhost:44382/api/donationdata/finddonation/{id}

            UpdateDonation ViewModel = new UpdateDonation();

            //the existing donation information
            string url = "donationdata/finddonation/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            DonationDto SelectedDonation = response.Content.ReadAsAsync<DonationDto>().Result;
            //Debug.WriteLine("Donation received : ");
            //Debug.WriteLine("selecteddonation.DonationAmount");
            
            ViewModel.SelectedDonation = SelectedDonation;

            //all funds to choose from when updating the donation
            //the existing animal information
            url = "funddata/listfunds/";
            response = client.GetAsync(url).Result;
            IEnumerable<FundDto> FundOptions  = response.Content.ReadAsAsync<IEnumerable<FundDto>>().Result;

            ViewModel.FundOptions = FundOptions;    

            return View(ViewModel);
        }

        // POST: Donation/Update/5
        [HttpPost]
        public ActionResult Update(int id, Donation donation)
        {
            string url = "donationdata/updatedonation/" + id;

            string jsonpayload = jss.Serialize(donation);

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

        // GET: Donation/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "donationdata/finddonation/" + id; 
            HttpResponseMessage response = client.GetAsync(url).Result;
            DonationDto selecteddonation = response.Content.ReadAsAsync<DonationDto>().Result;
            return View(selecteddonation);
        }

        // POST: Donation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "donationdata/deletedonation/" + id;
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
