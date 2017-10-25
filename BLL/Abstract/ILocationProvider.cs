using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface ILocationProvider
    {
        StatusCountryViewModel CountryAdd(CountryAddViewModel addCountry);
        CountryViewModel Countries(int page, SearchCountryViewModel search);
        StatusCountryViewModel CountryEdit(CountryEditViewModel editCountry);
        CountryEditViewModel GetCountryEditById(int id);
        
    }
}
