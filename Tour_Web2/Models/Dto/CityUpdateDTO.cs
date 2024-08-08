using System.ComponentModel.DataAnnotations;
namespace Tour_Web.Models.DTO
{
    public class CityUpdateDTO
    {
        
        [Required]
        public int CityNumber { get; set; }

        [Required]
        public int CountryID { get; set; }

        [MaxLength(20)]
        [Required]
        public string CityName { get; set; }

        public string CityDescription { get; set; }

        public string CityImageUrl { get; set; }

    }
}
