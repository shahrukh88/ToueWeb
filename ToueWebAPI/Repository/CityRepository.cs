using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ToueWebAPI.Data;
using ToueWebAPI.Models;
using ToueWebAPI.Repository.IRepository;

namespace ToueWebAPI.Repository
{
    public class CityRepository : Repository<City>,ICityRepository
    {
        private readonly ApplicationDbContext _db;


        public CityRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }

        
        public async Task<City> UpdateAsync(City entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Cities.Update(entity);
            await _db.SaveChangesAsync();
            return entity;

        }
    }
}
