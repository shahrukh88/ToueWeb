using System.Linq.Expressions;
using ToueWebAPI.Models;

namespace ToueWebAPI.Repository.IRepository
{
    public interface ICityRepository:IRepository<City>
    {

        Task<City> UpdateAsync(City city);
    }
}
