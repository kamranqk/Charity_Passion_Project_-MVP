using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PassionProjectChariity_s2022.Models
{
    public class Fund
    {
        // we are going to describe the Fund
        // primary key for Funf table
        [Key]
        public int FundID { get; set; }

        public string FundName { get; set; }


        // a sponor can donate to multiple funds
        // a fund can get donation from multiple sponsors

    }
    public class FundDto
    {
        public int FundID { get; set; }
        public string FundName { get; set; }
    }
}