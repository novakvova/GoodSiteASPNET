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
        
    }
}