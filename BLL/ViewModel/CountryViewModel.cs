using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class CountryItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreate { get; set; }
        public int Priority { get; set; }
    }
    public class CountryViewModel
    {
        public IEnumerable<CountryItemViewModel> Countries { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
    }
    public class CountryAddViewModel
    {
        public string Name { get; set; }
        public int Priority { get; set; }
    }
    public class CountryEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
    }
    public enum StatusCountryViewModel
    {
        Success = 0,
        Dublication = 1,
        Error=2
    }
}
