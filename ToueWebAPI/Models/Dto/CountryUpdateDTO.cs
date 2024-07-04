﻿using System.ComponentModel.DataAnnotations;
namespace ToueWebAPI.Models.DTO
{
    public class CountryUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Contenent { get; set; }

        public string ImageUrl { get; set; }

    }
}
