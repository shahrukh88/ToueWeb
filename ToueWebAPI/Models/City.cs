using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToueWebAPI.Models
{
    public class City
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int CityNumber { get; set; }

        [ForeignKey("Country")]
        public int CountryID { get; set; }

        public Country Country { get; set; }

        public string CityName { get; set; }

        public string CityDescription { get; set; }

        public string CityImageUrl { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
