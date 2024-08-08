using AutoMapper;
using Tour_Web.Models;
using Tour_Web.Models.DTO;

namespace Tour_Web
{
    public class MappingConfig: Profile
    {
        public MappingConfig() {

            CreateMap<CountryDTO, CountryCreateDTO>().ReverseMap();
            CreateMap<CountryDTO, CountryUpdateDTO>().ReverseMap();



            CreateMap<CityDTO, CityCreateDTO>().ReverseMap();
            CreateMap<CityDTO, CityUpdateDTO>().ReverseMap();
        }
    }

}
