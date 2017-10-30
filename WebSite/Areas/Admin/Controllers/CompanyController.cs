using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Areas.Admin.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Admin/Company
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            CompanyCreateViewModel model = new CompanyCreateViewModel();
            ViewBag.ListCities = new List<SelectItemViewModel> {
                new SelectItemViewModel{
                    Id=1,
                    Name="Москва"
                },
                new SelectItemViewModel{
                    Id=2,
                    Name="Київ"
                },
                new SelectItemViewModel{
                    Id=3,
                    Name="Рівне"
                },
                new SelectItemViewModel{
                    Id=4,
                    Name="Нью-Йорк"
                }
            };
            return View(model);
        }
    }
}