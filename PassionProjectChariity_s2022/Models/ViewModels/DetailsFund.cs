using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProjectChariity_s2022.Models.ViewModels
{
    public class DetailsFund
    {
        //the fund itself that we want to display
        public FundDto SelectedFund { get; set; }

        //all of the related animals to that particular species
        public IEnumerable<DonationDto> RelatedDonations { get; set; }
    }
}