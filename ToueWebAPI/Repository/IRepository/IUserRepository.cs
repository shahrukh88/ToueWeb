using MagicCountry_CountryAPI.Models;
using MagicCountry_CountryAPI.Models.Dto;

namespace MagicCountry_CountryAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);

        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);

        Task<LocalUser>Register(RegistrationRequestDTO registrationRequestDTO);
    }
}
