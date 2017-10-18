using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Controllers
{
    [Authorize]
    public class StuardesaController : Controller
    {
        
        // GET: Stuardesa
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles ="Admin")]
        public ActionResult Abort()
        {
            return View();
        }
        [Authorize(Roles = "Admin,Stuardesa")]
        public ActionResult Preview()
        {
            return View();
        }
    }
}