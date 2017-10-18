using BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ViewModel;
using DAL.Abstract;
using DAL.Entities;
using System.Web.Security;

namespace BLL.Concrete
{
    public class AccountProvider : IAccountProvider
    {
        private readonly IUserRepository _userRepository;
        public AccountProvider(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public StatusAccountViewModel Login(LoginViewModel model)
        {
            var user = _userRepository.GetUserByEmail(model.Email);
            if (user != null)
            {
                var crypto = new SimpleCrypto.PBKDF2();
                var password = crypto.Compute(model.Password, user.PasswordSalt);
                if (password == user.Password)
                {
                    FormsAuthentication
                                .SetAuthCookie(model.Email, model.IsRememberMe);
                    return StatusAccountViewModel.Success;
                }
            }
            return StatusAccountViewModel.Error;
        }

        public void Logout()
        {
            FormsAuthentication.SignOut(); 
        }

        public StatusAccountViewModel Register(RegisterViewModel model)
        {
            var userDub = _userRepository.GetUserByEmail(model.Email);
            if (userDub != null)
            {
                return StatusAccountViewModel.Dublication;
            }
            var crypto = new SimpleCrypto.PBKDF2();
            User user = new User();
            user.Email = model.Email;
            user.Password = crypto.Compute(model.Password);
            user.PasswordSalt = crypto.Salt;
            _userRepository.Add(user);
            _userRepository.SaveChange();

            return StatusAccountViewModel.Success;
        }

        public IEnumerable<string> UserRoles(string email)
        {
            var user= _userRepository.GetUserByEmail(email);
            return user.Roles.Select(r=>r.Name);
        }
    }
}
