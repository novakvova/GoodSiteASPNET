using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class CountryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreate { get; set; }
        public int Priority { get; set; }
    }
    public class AddCountryViewModel
    {
        public string Name { get; set; }
        public int Priority { get; set; }
    }
}
