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
using BLL.Infrastructure.Identity.Service;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using DAL.Entities.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace BLL.Concrete
{
    public class AccountIdentityProvider : IAccountProvider
    {
        private SignInService _signInManager;
        private UserService _userManager;

        public SignInService SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<SignInService>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public UserService UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().Get<UserService>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        public StatusAccountViewModel CreateLogin(string email)
        {
            var info = AuthenticationManager.GetExternalLoginInfo();
            var user = new AppUser { UserName = email, Email = email };
            var result = UserManager.Create(user);
            if (result.Succeeded)
            {
                result = UserManager.AddLogin(user.Id, info.Login);
                if (result.Succeeded)
                {
                    SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                    return StatusAccountViewModel.Success;
                }
            }
            return StatusAccountViewModel.Error;
        }

        public SignInStatus ExternalSignIn(ExternalLoginInfo loginInfo, bool isPersistent)
        {
            return SignInManager.ExternalSignIn(loginInfo, isPersistent: false);
        }

        public ExternalLoginInfo GetExternalLoginInfo()
        {
            return AuthenticationManager.GetExternalLoginInfo();
        }

        public StatusAccountViewModel Login(LoginViewModel model)
        {
            var result = SignInManager
                .PasswordSignIn(model.Email,model.Password,model.IsRememberMe,shouldLockout:false);
            switch (result)
            {
                case SignInStatus.Success:
                    return StatusAccountViewModel.Success;
            }
            return StatusAccountViewModel.Error;
        }

        

        public void Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie); 
        }

        public StatusAccountViewModel Register(RegisterViewModel model)
        {
            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = UserManager.Create(user, model.Password);
            if (result.Succeeded)
            {
                return StatusAccountViewModel.Success;
            }

            return StatusAccountViewModel.Error;
        }

        public bool SendTwoFactorCode(string provider)
        {
            return SignInManager.SendTwoFactorCode(provider);
        }

        public IList<string> UserFactors()
        {
            var userId = SignInManager.GetVerifiedUserId();
            if (userId == null)
            {
                return null;
            }
            var userFactors = UserManager.GetValidTwoFactorProviders(userId);
            return userFactors;
        }

        

        public IEnumerable<string> UserRoles(string email)
        {
            throw new NotImplementedException();
            //var user= _userRepository.GetUserByEmail(email);
            //return user.Roles.Select(r=>r.Name);
        }
    }
}
