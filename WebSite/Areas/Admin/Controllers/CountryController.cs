using BLL.Abstract;
using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Areas.Admin.Controllers
{
    public class CountryController : Controller
    {
        private readonly ILocationProvider _locationProvider;
        public CountryController(ILocationProvider locationProvider)
        {
            _locationProvider = locationProvider;
        }
        // GET: Admin/Location
        public ActionResult Index(SearchCountryViewModel search, int page = 1)
        {
            var model = _locationProvider.Countries(page, search);
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(CountryAddViewModel country)
        {
            if (ModelState.IsValid)
            {
                var status = _locationProvider.CountryAdd(country);
                if (status == StatusCountryViewModel.Success)
                    return RedirectToAction("Index");
                else if (status == StatusCountryViewModel.Dublication)
                    ModelState.AddModelError("", "Країна за даним іменем уже існує!");
            }
            return View(country);
        }
        public ActionResult Edit(int id)
        {
            var model = _locationProvider.GetCountryEditById(id);
            return View(model);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(CountryEditViewModel country)
        {
            if (ModelState.IsValid)
            {
                var status = _locationProvider.CountryEdit(country);
                if (status == StatusCountryViewModel.Success)
                    return RedirectToAction("Index");
                else if (status == StatusCountryViewModel.Dublication)
                    ModelState.AddModelError("", "Країна за даним іменем уже існує!");
                else if (status == StatusCountryViewModel.Error)
                    ModelState.AddModelError("", "Помилка редагування");
            }
            return View(country);
        }
    }
}