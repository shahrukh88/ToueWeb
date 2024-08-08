using AutoMapper;
using ToueWebAPI.Models;
using ToueWebAPI.Models.DTO;

namespace ToueWebAPI
{
    public class MappingConfig: Profile
    {
        public MappingConfig() {

            CreateMap<Country, CountryDTO>();
            CreateMap<CountryDTO, Country>();

            CreateMap<Country, CountryCreateDTO>().ReverseMap();
            CreateMap<Country, CountryUpdateDTO>().ReverseMap();


            CreateMap<City, CityDTO>().ReverseMap();
            CreateMap<City, CityCreateDTO>().ReverseMap();
            CreateMap<City, CityUpdateDTO>().ReverseMap();
        }
    }

}
