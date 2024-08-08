using Tour_Web.Models.DTO;

namespace Tour_Web.Services.IServices
{
    public interface ICityService
    {
        Task<T> GetAllAsync<T>();

        Task<T> GetAsync<T>(int id);

        Task<T> CreateAsync<T>(CityCreateDTO dto);

        Task<T> UpdateAsync<T>(CityUpdateDTO dto);

        Task<T> DeleteAsync<T>(int id);
    }
}
