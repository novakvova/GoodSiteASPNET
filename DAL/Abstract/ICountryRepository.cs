using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface ICountryRepository : IDisposable
    {
        Country Add(Country country);
        IQueryable<Country> GetAllCountries();
        void SaveChange();
    }
}
