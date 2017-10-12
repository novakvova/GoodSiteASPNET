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
        public StatusCountryViewModel CountryAdd(CountryAddViewModel addCountry)
        {
            var searchCountry = _countryRepository.GetCountryByName(addCountry.Name);
            if (searchCountry != null)
                return StatusCountryViewModel.Dublication;

            Country country = new Country
            {
                Name = addCountry.Name,
                DateCreate = DateTime.Now,
                Priority = addCountry.Priority
            };
            _countryRepository.Add(country);
            _countryRepository.SaveChange();

            return StatusCountryViewModel.Success;
        }

        public CountryViewModel Countries(int page)
        {
            int pageSize = 10;
            int pageNo = page - 1;
            CountryViewModel model = new CountryViewModel();

            model.Countries = _countryRepository
                .GetAllCountries()
                .OrderBy(c => c.Id)
                .Skip(pageNo * pageSize)
                .Take(pageSize)
                .Select(c => new CountryItemViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    DateCreate = c.DateCreate,
                    Priority = c.Priority
                });
            int count=_countryRepository.TotalCountries();
            model.TotalPage = (int)Math.Ceiling((double)count/pageSize);
            model.CurrentPage = page;
            return model;
        }

        public StatusCountryViewModel CountryEdit(CountryEditViewModel editCountry)
        {
            try
            {
                var dub = _countryRepository.GetCountryByName(editCountry.Name);
                if (dub != null && dub.Id != editCountry.Id)
                    return StatusCountryViewModel.Dublication;
                var country = _countryRepository.GetCountryById(editCountry.Id);
                if (country != null)
                {
                    country.Name = editCountry.Name;
                    country.Priority = editCountry.Priority;
                    _countryRepository.SaveChange();
                    return StatusCountryViewModel.Success;
                }
            }
            catch { }
            return StatusCountryViewModel.Error;
        }


        public CountryEditViewModel GetCountryEditById(int id)
        {
            CountryEditViewModel model = null;
            var country = _countryRepository.GetCountryById(id);
            if (country != null)
            {
                model = new CountryEditViewModel
                {
                    Id = country.Id,
                    Name = country.Name,
                    Priority = country.Priority
                };
            }
            return model;
        }
    }
}
