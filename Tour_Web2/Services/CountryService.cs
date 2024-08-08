using Tour_Web.Models;
using Tour_Web.Models.DTO;
using Tour_Web.Services.IServices;
using Tour_Web_Utility;

namespace Tour_Web.Services
{
    public class CountryService : BaseService, ICountryService
    {

        private readonly IHttpClientFactory _clientFactory;
        private string countryUrl;

        public CountryService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            countryUrl = configuration.GetValue<string>("ServiceUrls:CountryAPI");
        }


        public Task<T> CreateAsync<T>(CountryCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = countryUrl + "/api/countryAPI"
            }); 
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,

                Url = countryUrl + "/api/countryAPI/" + id
            }); 
        }

        

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
               
                Url = countryUrl + "/api/countryAPI"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,

                Url = countryUrl + "/api/countryAPI/" + id
            });
        }

        public Task<T> UpdateAsync<T>(CountryUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = countryUrl + "/api/countryAPI/" + dto.Id
            });
        }
    }
}
