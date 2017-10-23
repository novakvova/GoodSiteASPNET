﻿using BLL.Abstract;
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

namespace BLL.Concrete
{
    public class AccountIdentityProvider : IAccountProvider
    {
        private SignInService _signInManager;
        private UserService _userManager;
        public AccountIdentityProvider(IUserRepository userRepository)
        {
            
            //_userRepository = userRepository;
        }

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
            FormsAuthentication.SignOut(); 
        }

        public StatusAccountViewModel Register(RegisterViewModel model)
        {
            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = UserManager.CreateAsync(user, model.Password).Result;
            if (result.Succeeded)
            {
                return StatusAccountViewModel.Success;
            }

            return StatusAccountViewModel.Error;
        }

        public IEnumerable<string> UserRoles(string email)
        {
            throw new NotImplementedException();
            //var user= _userRepository.GetUserByEmail(email);
            //return user.Roles.Select(r=>r.Name);
        }
    }
}
