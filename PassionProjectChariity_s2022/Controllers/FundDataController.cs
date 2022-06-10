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
    public class FundDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all fund in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Donations in the database, including their associated fund.
        /// </returns>
        /// <example>
        /// GET: api/FundData/ListFunds
        /// </example>
        [HttpGet]
        [ResponseType(typeof(FundDto))]
        public IHttpActionResult ListFunds()
        {
            List<Fund> Funds = db.Funds.ToList();
            List<FundDto> FundDtos = new List<FundDto>();

            Funds.ForEach(f => FundDtos.Add(new FundDto()
            {
                FundID = f.FundID,
                FundName = f.FundName,
            }));

            return Ok(FundDtos);
        }

        /// <summary>
        /// Returns all funds in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An Fund in the system matching up to the Fund ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the Fund</param>
        /// <example>
        /// GET: api/FundData/FindFund/5
        /// </example>
        [ResponseType(typeof(FundDto))]
        [HttpGet]
        public IHttpActionResult FindFund(int id)
        {
            Fund Fund = db.Funds.Find(id);
            FundDto FundDto = new FundDto()
            {
                FundID = Fund.FundID,
                FundName = Fund.FundName,
                
            };
            if (Fund == null)
            {
                return NotFound();
            }

            return Ok(FundDto);
        }

        /// <summary>
        /// Updates a particular Fund in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Fund ID primary key</param>
        /// <param name="Fund">JSON FORM DATA of an Fund</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/FundData/UpdateFund/5
        /// FORM DATA: Fund JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateFund(int id, Fund fund)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fund.FundID)
            {

                return BadRequest();
            }

            db.Entry(fund).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FundExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Adds an Fund to the system
        /// </summary>
        /// <param name="Fund">JSON FORM DATA of an Fund</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Fund ID, Fund Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/FundData/AddFund
        /// FORM DATA: Fund JSON Object
        /// </example>
        [ResponseType(typeof(Fund))]
        [HttpPost]
        public IHttpActionResult AddFund(Fund fund)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Debug.WriteLine("I have reached the update Fund method!");
            db.Funds.Add(fund);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fund.FundID }, fund);
        }

        /// <summary>
        /// Deletes an Fund from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the Fund</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/FundData/DeleteFund/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Fund))]
        [HttpPost]
        public IHttpActionResult DeleteFund(int id)
        {
            Fund Fund = db.Funds.Find(id);
            if (Fund == null)
            {
                return NotFound();
            }

            db.Funds.Remove(Fund);
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

        private bool FundExists(int id)
        {
            return db.Funds.Count(e => e.FundID == id) > 0;
        }
    }
}
