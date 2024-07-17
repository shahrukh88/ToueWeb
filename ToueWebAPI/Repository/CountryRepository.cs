using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ToueWebAPI.Data;
using ToueWebAPI.Models;
using ToueWebAPI.Repository.IRepository;

namespace ToueWebAPI.Repository
{
    public class CountryRepository : Repository<Country>,ICountryRepository
    {
        private readonly ApplicationDbContext _db;


        public CountryRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }

        
        public async Task<Country> UpdateAsync(Country entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Countries.Update(entity);
            await _db.SaveChangesAsync();
            return entity;

        }
    }
}
