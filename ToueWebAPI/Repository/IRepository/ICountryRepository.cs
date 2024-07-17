using System.Linq.Expressions;
using ToueWebAPI.Models;

namespace ToueWebAPI.Repository.IRepository
{
    public interface ICountryRepository:IRepository<Country>
    {

        Task<Country> UpdateAsync(Country country);
    }
}
