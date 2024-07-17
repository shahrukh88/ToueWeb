﻿using System.ComponentModel.DataAnnotations;
namespace ToueWebAPI.Models.DTO
{
    public class CityDTO
    {

        [Required]
        public int CityNumber { get; set; }

        [Required]
        public int CountryID { get; set; }

        public string CityName { get; set; }

        public string CityDescription { get; set; }

        public string CityImageUrl { get; set; }

    }
}
