using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PassionProjectChariity_s2022.Models;
using System.Diagnostics;

namespace PassionProjectChariity_s2022.Controllers
{
    public class DonationDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: api/DonationData/ListDonations
        [HttpGet]
        [ResponseType(typeof(DonationDto))]
        public IEnumerable<DonationDto> ListDonations()
        {
            List<Donation> Donations = db.Donations.ToList();
            List<DonationDto> DonationDtos = new List<DonationDto>();

            Donations.ForEach(a => DonationDtos.Add(new DonationDto(){
                DonationID = a.DonationID,
                DonationDate = a.DonationDate,
                DonationAmount = a.DonationAmount,
                FundName = a.Fund.FundName
            }));

            return DonationDtos;
        }
        /// <summary>
        /// Gathers information about all donation related to a particular fund ID
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all donations in the database, including their associated fund matched with a particular fund ID
        /// </returns>
        /// <param name="id">Fund ID.</param>
        /// <example>
        /// GET: api/DonationData/ListDonationsForFund/3
        /// </example>
        [HttpGet]
        [ResponseType(typeof(DonationDto))]
        public IEnumerable<DonationDto> ListDonationsForFund(int id)
        {
            List<Donation> Donations = db.Donations.Where(a=>a.FundID==id).ToList();
            List<DonationDto> DonationDtos = new List<DonationDto>();

            Donations.ForEach(a => DonationDtos.Add(new DonationDto(){
                DonationID = a.DonationID,
                DonationDate = a.DonationDate,
                DonationAmount = a.DonationAmount,
                FundName = a.Fund.FundName
            }));

            return DonationDtos;
        }

        // GET: api/DonationData/FindDonation/3
        [ResponseType(typeof(Donation))]
        [HttpGet]
        public IHttpActionResult FindDonation(int id)
        {
            Donation Donation = db.Donations.Find(id);
            DonationDto DonationDto = new DonationDto()
            {
                DonationID = Donation.DonationID,
                DonationDate = Donation.DonationDate,
                DonationAmount = Donation.DonationAmount,
                FundName = Donation.Fund.FundName
            };
            if (Donation == null)
            {
                return NotFound();
            }

            return Ok(DonationDto);
        }

        // POST: api/DonationData/UpdateDonation/1
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateDonation(int id, Donation donation)
        {
            Debug.WriteLine("I have reached the update animal method!");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model state is invalid");
                return BadRequest(ModelState);
            }

            if (id != donation.DonationID)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter"+id);
                Debug.WriteLine("POST parameter"+ donation.DonationID);
                Debug.WriteLine("POST parameter" + donation.DonationDate);
                Debug.WriteLine("POST parameter" + donation.DonationAmount);
                return BadRequest();
            }

            db.Entry(donation).State  = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonationExists(id))
                {
                    Debug.WriteLine("Donation not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Debug.WriteLine("None of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/DonationData/AddDonation
        [ResponseType(typeof(Donation))]
        [HttpPost]
        public IHttpActionResult AddDonation(Donation donation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Donations.Add(donation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = donation.DonationID }, donation);
        }

        // POST: api/DonationData/Delete/1
        [ResponseType(typeof(Donation))]
        [HttpPost]
        public IHttpActionResult DeleteDonation(int id)
        {
            Donation donation = db.Donations.Find(id);
            if (donation == null)
            {
                return NotFound();
            }

            db.Donations.Remove(donation);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DonationExists(int id)
        {
            return db.Donations.Count(e => e.DonationID == id) > 0;
        }
    }
}