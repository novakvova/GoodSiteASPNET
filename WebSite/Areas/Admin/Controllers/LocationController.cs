using BLL.Abstract;
using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Areas.Admin.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocationProvider _locationProvider;
        public LocationController(ILocationProvider locationProvider)
        {
            _locationProvider = locationProvider;
        }
        // GET: Admin/Location
        public ActionResult Index()
        {
            var model = _locationProvider.Countries();
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(AddCountryViewModel country)
        {
            if(ModelState.IsValid)
            {
                var status=_locationProvider.AddCountry(country);
                if (status == StatusCountryViewModel.Success)
                    return RedirectToAction("Index");
                else if (status == StatusCountryViewModel.Dublication)
                    ModelState.AddModelError("", "Країна за даним іменем уже існує!");
            }
            return View(country);
        }
    }
}