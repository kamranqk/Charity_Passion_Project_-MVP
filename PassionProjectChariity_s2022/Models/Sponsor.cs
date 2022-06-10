using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PassionProjectChariity_s2022.Models
{
    public class Sponsor
    {
        // we are going to describe the Sponsor
        // primary key for Sponsor table
        [Key]
        public int SponsorID { get; set; }

        public string SponsorFirstName { get; set; }

        public string SponsorLastName { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

        // a sponor can donate to multiple funds
        // a fund can get donation from multiple sponsors
        public ICollection<Donation> Donations { get; set; }
    }

    
}