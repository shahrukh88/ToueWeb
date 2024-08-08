using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tour_Web.Models.DTO;

namespace Tour_Web.Models.Models.VM
{
    public class CityCreateVM
    {
        public CityCreateVM() 
        {

            CityNumber = new CityCreateDTO();
        }

        public CityCreateDTO CityNumber { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CountryList { get; set; }
    }
}
