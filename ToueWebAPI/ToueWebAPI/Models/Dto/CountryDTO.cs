using System.ComponentModel.DataAnnotations;
namespace ToueWebAPI.Models.DTO
{
    public class CountryDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
