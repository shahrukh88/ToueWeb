using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tour_Web.Models.DTO;

namespace Tour_Web.Models.Models.VM
{
    public class CityUpdateVM
    {
        public CityUpdateVM() 
        {

            CityNumber = new CityUpdateDTO();
        }

        public CityUpdateDTO CityNumber { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CountryList { get; set; }
    }
}
