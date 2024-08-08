using Tour_Web.Models;
using Tour_Web.Models.DTO;
using Tour_Web.Services.IServices;
using Tour_Web_Utility;

namespace Tour_Web.Services
{
    public class CityService : BaseService, ICityService
    {

        private readonly IHttpClientFactory _clientFactory;
        private string countryUrl;

        public CityService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            countryUrl = configuration.GetValue<string>("ServiceUrls:CountryAPI");
        }


        public Task<T> CreateAsync<T>(CityCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = countryUrl + "/api/CityAPI"
            }); 
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,

                Url = countryUrl + "/api/CityAPI/" + id
            }); 
        }

        

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
               
                Url = countryUrl + "/api/CityAPI"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,

                Url = countryUrl + "/api/CityAPI/" + id
            });
        }

        public Task<T> UpdateAsync<T>(CityUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = countryUrl + "/api/CityAPI/" + dto.CityNumber
            });
        }
    }
}
