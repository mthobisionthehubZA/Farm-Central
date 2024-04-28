using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Farmers_App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }


        public ActionResult Contact()
        {
            
            return View();
        }

        // Privacy controller that will return a view page of the users privacy 
        
        public ActionResult Privacy()
        {
            // Note: MVC5 VIEW, empty without model
            return View();
        }

     
    }
}