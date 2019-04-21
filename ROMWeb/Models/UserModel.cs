using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ROM.Models
{
    public class UserModel
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "User Description")]
        public string UserDescription { get; set; }

        [Display(Name = "Country")]
        public string CountryName { get; set; }

        [Display(Name = "State / Province / Region")]
        public string RegionName { get; set; }

        [Display(Name = "Start Date")]
        public string StartDate { get; set; }
    }
}