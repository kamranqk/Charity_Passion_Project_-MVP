using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProjectChariity_s2022.Models.ViewModels
{
    public class UpdateDonation
    {
        //This viewmodel is a class which stores information that we need to present to /Donation/Update/{}

        //the existing Donation information

        public DonationDto SelectedDonation { get; set; }

        // all Funds to choose from when updating the Donation

        public IEnumerable<FundDto> FundOptions { get; set; }
    }
}