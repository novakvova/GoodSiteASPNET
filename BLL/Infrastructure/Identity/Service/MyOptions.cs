using DAL.Entities.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure.Identity.Service
{
    public static class MyOptions
    {
        public static CookieAuthenticationOptions OptionCookies()
        {
            return new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<UserService, AppUser>(
                        validateInterval: TimeSpan.FromMinutes(365),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            };
        }
    }
}
