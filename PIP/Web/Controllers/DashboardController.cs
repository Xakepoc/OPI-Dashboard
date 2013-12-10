using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            var model = new DashboardModel();
            model.Init();
            return View("Index", model);
        }

        public ActionResult Projects(string region, string country, string impactArea, string coreCompetency)
        {
            var model = new ProjectListingModel();
            model.Init(region, country, impactArea, coreCompetency);
            return View("Projects", model);
        }

        public ActionResult Project(string zcode)
        {
            var model = new ProjectModel();
            model.Init(zcode);
            return View("ProjectDetails", model);
        }
    }
}
