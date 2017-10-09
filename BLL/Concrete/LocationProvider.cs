using BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ViewModel;
using DAL.Abstract;
using DAL.Entities;

namespace BLL.Concrete
{
    public class LocationProvider : ILocationProvider
    {
        private readonly ICountryRepository _countryRepository;
        public LocationProvider(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public int AddCountry(AddCountryViewModel addCountry)
        {
            Country country = new Country {
                Name=addCountry.Name,
                DateCreate=DateTime.Now,
                Priority=addCountry.Priority
            };
            _countryRepository.Add(country);
            _countryRepository.SaveChange();
            return country.Id;
        }

        public IEnumerable<CountryViewModel> Countries()
        {
            var countries= _countryRepository.GetAllCountries()
                .Select(c=>new CountryViewModel {
                    Id=c.Id,
                    Name=c.Name,
                    DateCreate=c.DateCreate,
                    Priority=c.Priority
                });
            return countries.AsEnumerable();
        }
    }
}
