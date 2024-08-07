﻿using System.ComponentModel.DataAnnotations;

namespace Form.Areas.LOC_Contry.Models
{
    public class LOC_CountryModel
    {
        public int? CountryID { get; set; }
        [Required]
        public string CountryName { get; set; }

        [Required]
        public string  CountryCode { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class LOC_CountryDropDownModel
    {
        public int? CountryID { get; set; }

        [Required]
        public string? CountryName { get; set; }
    }
}
