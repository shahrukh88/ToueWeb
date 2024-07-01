using ToueWebAPI.Models.DTO;

namespace ToueWebAPI.Data
{
    public static class CountryStore
    {

        public static List<CountryDTO> countryList = new List<CountryDTO>
            {
                new CountryDTO{Id=1,Name="Pak"},
                new CountryDTO{Id=2,Name="Eng"}
            };
    }
}
