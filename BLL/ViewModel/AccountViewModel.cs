using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class LoginViewModel
    {
        [Required, EmailAddress, Display(Name ="Електронна пошта")]
        public string Email { get; set; }
        [Required, DataType(DataType.Password), Display(Name ="Пароль")]
        public string Password { get; set; }
        [Display(Name ="Запам'ятати мене")]
        public bool IsRememberMe { get; set; }
    }
}
