using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToueWebAPI.Models
{
    public class Country
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Contenent { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedDate {  get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
