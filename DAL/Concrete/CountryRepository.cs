using DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Concrete
{
    public class CountryRepository : ICountryRepository
    {
        private readonly IEFContext _context;
        public CountryRepository(IEFContext context)
        {
            _context = context;
        }
        public Country Add(Country country)
        {
            _context.Set<Country>().Add(country);
            return country;
        }

        public void Dispose()
        {
            if(this._context!=null)
                this._context.Dispose();
        }

        public IQueryable<Country> GetAllCountries()
        {
            return this._context.Set<Country>();
        }

        public Country GetCountryByName(string name)
        {
            return this.GetAllCountries()
                .SingleOrDefault(c=>c.Name==name);
        }

        public void SaveChange()
        {
            this._context.SaveChanges();
        }
    }
}
