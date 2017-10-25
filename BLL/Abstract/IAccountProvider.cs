using BLL.ViewModel;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IAccountProvider
    {
        StatusAccountViewModel Register(RegisterViewModel model);
        StatusAccountViewModel Login(LoginViewModel model);
        StatusAccountViewModel CreateLogin(string email);
        IEnumerable<string> UserRoles(string email);
        IList<string> UserFactors();
        bool SendTwoFactorCode(string provider);
        ExternalLoginInfo GetExternalLoginInfo();
        SignInStatus ExternalSignIn(ExternalLoginInfo loginInfo, bool isPersistent);
        void Logout();
        
    }
}
