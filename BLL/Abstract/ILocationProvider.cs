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
        int AddCountry(AddCountryViewModel addCountry);
        IEnumerable<CountryViewModel> Countries(); 
    }
}
