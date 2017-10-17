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
    public class AccountProvider : IAccountProvider
    {
        private readonly IUserRepository _userRepository;
        public AccountProvider(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public StatusAccountViewModel Register(RegisterViewModel model)
        {
            var userDub=_userRepository.GetUserByEmail(model.Email);
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
    }
}
