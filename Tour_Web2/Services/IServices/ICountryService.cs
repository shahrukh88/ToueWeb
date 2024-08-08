using Tour_Web.Models.DTO;

namespace Tour_Web.Services.IServices
{
    public interface ICountryService
    {
        Task<T> GetAllAsync<T>();

        Task<T> GetAsync<T>(int id);

        Task<T> CreateAsync<T>(CountryCreateDTO dto);

        Task<T> UpdateAsync<T>(CountryUpdateDTO dto);

        Task<T> DeleteAsync<T>(int id);
    }
}
