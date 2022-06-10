using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProjectChariity_s2022.Models
{
    public class Donation
    {

        // we are going to describe the Donation
        // primary key for Donation table
        [Key]
        public int DonationID { get; set; }

        public DateTime DonationDate { get; set; }

        public int DonationAmount { get; set; }


        [ForeignKey("Fund")]
        public int FundID { get; set; }
        public virtual Fund Fund { get; set; }

        // a sponor can donate to multiple funds
        // a fund can get donation from multiple sponsors

        public ICollection<Sponsor> Sponsors { get; set; }
    }

    public class DonationDto
    {
        public int DonationID { get; set; }

        public DateTime DonationDate { get; set; }

        public int DonationAmount { get; set; }

        public int FundID { get; set; }
        public string FundName { get; set; }


    }
}