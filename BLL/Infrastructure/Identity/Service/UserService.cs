using DAL.Entities;
using DAL.Entities.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure.Identity.Service
{
    public class UserService : UserManager<AppUser>
    {
        public UserService(IUserStore<AppUser> store) : base(store) { }
        public static UserService Create(IdentityFactoryOptions<UserService> options,
            IOwinContext context)
        {
            var service = new UserService(new UserStore<AppUser>(context.Get<EFContext>()));

            service.UserValidator = new UserValidator<AppUser>(service)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            service.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            service.UserLockoutEnabledByDefault = true;
            service.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            service.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            //service.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<User>
            //{
            //    MessageFormat = "Your security code is {0}"
            //});
            service.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<AppUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            service.EmailService = new EmailService();
            //service.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                service.UserTokenProvider =
                    new DataProtectorTokenProvider<AppUser>(dataProtectionProvider.Create("OBK project security Identity"));
            }
            return service;
        }
    }
}
