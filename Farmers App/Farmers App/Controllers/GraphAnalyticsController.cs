using Farmers_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Farmers_App.Controllers
{


    public class GraphAnalyticsController : Controller
    {
        // Database context for accessing farmer data
        Farmer_V2Entities1 context = new Farmer_V2Entities1();

        // GET: GraphAnalytics
        public ActionResult Index()
        {
            return View();
        }

        //Method responsible for counting gender ratio of SA farmers    
        public ActionResult GetData() 
        {

            // Count the number of male farmers
            int male = context.FARMERS.Where(x => x.GENDER == "Male").Count();
            // Count the number of female farmers
            int female = context.FARMERS.Where(x => x.GENDER == "Female").Count();
            // Count the number of farmers with unspecified gender
            int Unspecified = context.FARMERS.Where(x => x.GENDER == "Unspecified").Count();

            // Create a Ratio object to store gender ratio data
            Ratio obj = new Ratio();
            obj.Male = male;
            obj.Female = female;
            obj.Unspecified = Unspecified;

            // Return the gender ratio data as JSON
            return Json(obj, JsonRequestBehavior.AllowGet);

        }

        // Class to represent the gender ratio
        public class Ratio
        {
            public int Male { get; set; }
            public int Female { get; set; }
            public int Unspecified { get; set; }
        }

        // Method to retrieve the count of farmers based on their type of farming
        public ActionResult GetFarmingType() 
        {
            // Count the number of farmers for each type of farming using LINQ queries.
            int arable = context.FARMERS.Where(x => x.TYPE_OF_FARMING == "Arable").Count();
            int commercial = context.FARMERS.Where(x => x.TYPE_OF_FARMING == "Commercial").Count();
            int extensive= context.FARMERS.Where(x => x.TYPE_OF_FARMING == "Extensive").Count();
            int intensive = context.FARMERS.Where(x => x.TYPE_OF_FARMING == "Intensive").Count();
            int mixed = context.FARMERS.Where(x => x.TYPE_OF_FARMING == "mixed").Count();
            int postoral = context.FARMERS.Where(x => x.TYPE_OF_FARMING == "Postoral").Count();
            int sedentary = context.FARMERS.Where(x => x.TYPE_OF_FARMING == "Sedentary").Count();
            int subsistence = context.FARMERS.Where(x => x.TYPE_OF_FARMING == "Subsistence").Count();
            
            // Create a new TypeOfFarming object and populate it with the counts.
            TypeOfFarming obj = new TypeOfFarming();
            obj.Arable = arable;
            obj.Commercial = commercial;
            obj.Extensive = extensive;
            obj.Intensive = intensive;
            obj.Mixed = mixed;
            obj.Postoral = postoral;
            obj.Sedentary = sedentary;
            obj.Subsistence = subsistence;

            // Return the counts as JSON.
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        // Class to represent the counts of farmers for each type of farming
        public class TypeOfFarming 
        {
            public int Arable { get; set; }
            public int Commercial { get; set; }
            public int Extensive { get; set; }
            public int Intensive { get; set; }
            public int Mixed { get; set; }
            public int Postoral { get; set; }
            public int Sedentary { get; set; }
            public int Subsistence { get; set; }
        }


        // Method to retrieve the count of farmers based on province
        public ActionResult GetProvince() 
        {
            // Count the number of farmers in each province
            int EC = context.FARMERS.Where(x => x.PROVINCE == "Eastern Cape").Count();
            int FS = context.FARMERS.Where(x => x.PROVINCE == "Free State").Count();
            int GP = context.FARMERS.Where(x => x.PROVINCE == "Gauteng").Count();
            int KZN = context.FARMERS.Where(x => x.PROVINCE == "KwaZulu-Natal").Count();
            int LP = context.FARMERS.Where(x => x.PROVINCE == "Limpopo").Count();
            int MP = context.FARMERS.Where(x => x.PROVINCE == "Mpumalanga").Count();
            int NW = context.FARMERS.Where(x => x.PROVINCE == "North West").Count();
            int NC = context.FARMERS.Where(x => x.PROVINCE == "Northen Cape").Count();
            int WC = context.FARMERS.Where(x => x.PROVINCE == "Western Cape").Count();

            // Create a new Province object and populate it with the counts.
            Province obj = new Province();
            obj.EasterCape = EC;
            obj.FreeState = FS;
            obj.Gauteng = GP;
            obj.KwaZuluNatal = KZN;
            obj.Limpopo = LP;
            obj.Mpumalanga = MP;
            obj.NorthWest = NW;
            obj.NorthenCape = NC;
            obj.WesternCape = WC;

            // Return the counts as JSON.
            return Json(obj, JsonRequestBehavior.AllowGet);

        }

        // Class to represent the counts of farmers in each province
        public class Province 
        {
            public int EasterCape { get; set; }
            public int FreeState { get; set; }
            public int Gauteng { get; set; }
            public int KwaZuluNatal { get; set; }
            public int Limpopo { get; set; }
            public int Mpumalanga { get; set; }
            public int NorthWest { get; set; }
            public int NorthenCape { get; set; }
            public int WesternCape { get; set; }

        }
    }
}