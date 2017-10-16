using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebSite.Controllers
{
    class DBUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class AccountController : Controller
    {
        private static List<DBUser> _listUsers;
        public AccountController()
        {
            _listUsers = new List<DBUser>
            {
                new DBUser { Email="ivanbalaban@i.ua", Password="123456"},
                new DBUser { Email="admin@gmail.com", Password="123456"}
            };
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = _listUsers
                    .SingleOrDefault(u=> u.Email==model.Email
                    && u.Password==model.Password);
                if (user != null)
                {
                    FormsAuthentication
                        .SetAuthCookie(user.Email, model.IsRememberMe);
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Не коректні дані!");
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}