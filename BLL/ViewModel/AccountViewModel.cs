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
        [Required, EmailAddress, Display(Name = "Електронна пошта")]
        public string Email { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Пароль")]
        public string Password { get; set; }
        [Display(Name = "Запам'ятати мене")]
        public bool IsRememberMe { get; set; }
    }
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
    public enum StatusAccountViewModel
    {
        Success = 0,
        Dublication = 1,
        Error = 2
    }
}
