using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Farmers_App.Models;
using PagedList; 

namespace Farmers_App.Controllers
{
    public class FarmerController : Controller
    {
        private Farmer_V2Entities1 db = new Farmer_V2Entities1();

        // GET: Farmer
        public ActionResult Index(string searchString, int? page)
        {

            var fARMERS = db.FARMERS.Include(f => f.FARM_TYPES).Include(f => f.GENDER1).Include(f => f.PROVINCE1);
            
            if (!string.IsNullOrEmpty(searchString))
            {
                TempData["SearchAlert"] = "No data Found";
                fARMERS = fARMERS.Where(f => f.FirstName.Contains(searchString) || f.LastName.Contains(searchString));

            }
            //Pagination setup
            int pageSize = 5; // Set your desired page size here
            int pageNumber = (page ?? 1);

            // Add OrderBy clause before using ToPagedList to avoid method skip errors
            fARMERS = fARMERS.OrderBy(f => f.FirstName); // sorting criteria by firstname 
            return View(fARMERS.ToPagedList(pageNumber, pageSize));
            
        }

       

        // GET: Farmer/Create
        public ActionResult Create()
        {
            ViewBag.TYPE_OF_FARMING = new SelectList(db.FARM_TYPES, "TYPE_OF_FARMING", "TYPE_OF_FARMING");
            ViewBag.GENDER = new SelectList(db.GENDERS, "GENDER1", "GENDER1");
            ViewBag.PROVINCE = new SelectList(db.PROVINCES, "PROVINCE1", "PROVINCE1");
            return View();
        }

        // POST: Farmer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,FirstName,LastName,Village_Name,TYPE_OF_FARMING,PROVINCE,GENDER")] FARMER fARMER)
        {
            if (ModelState.IsValid)
            {
                db.FARMERS.Add(fARMER);
                db.SaveChanges();
                TempData["AlertMessage"] = "Farmer Created Successfully...!";
                return RedirectToAction("Index");
            }

            ViewBag.TYPE_OF_FARMING = new SelectList(db.FARM_TYPES, "TYPE_OF_FARMING", "TYPE_OF_FARMING", fARMER.TYPE_OF_FARMING);
            ViewBag.GENDER = new SelectList(db.GENDERS, "GENDER1", "GENDER1", fARMER.GENDER);
            ViewBag.PROVINCE = new SelectList(db.PROVINCES, "PROVINCE1", "PROVINCE1", fARMER.PROVINCE);
            return View(fARMER);
        }

        // GET: Farmer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FARMER fARMER = db.FARMERS.Find(id);
            if (fARMER == null)
            {
                return HttpNotFound();
            }
            ViewBag.TYPE_OF_FARMING = new SelectList(db.FARM_TYPES, "TYPE_OF_FARMING", "TYPE_OF_FARMING", fARMER.TYPE_OF_FARMING);
            ViewBag.GENDER = new SelectList(db.GENDERS, "GENDER1", "GENDER1", fARMER.GENDER);
            ViewBag.PROVINCE = new SelectList(db.PROVINCES, "PROVINCE1", "PROVINCE1", fARMER.PROVINCE);
            return View(fARMER);
        }

        // POST: Farmer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,FirstName,LastName,Village_Name,TYPE_OF_FARMING,PROVINCE,GENDER")] FARMER fARMER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fARMER).State = EntityState.Modified;
                db.SaveChanges();
                TempData["AlertMessage"] = "Farmer details Updated Successfully...!";
                return RedirectToAction("Index");
            }
            ViewBag.TYPE_OF_FARMING = new SelectList(db.FARM_TYPES, "TYPE_OF_FARMING", "TYPE_OF_FARMING", fARMER.TYPE_OF_FARMING);
            ViewBag.GENDER = new SelectList(db.GENDERS, "GENDER1", "GENDER1", fARMER.GENDER);
            ViewBag.PROVINCE = new SelectList(db.PROVINCES, "PROVINCE1", "PROVINCE1", fARMER.PROVINCE);
            return View(fARMER);
        }

        // GET: Farmer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FARMER fARMER = db.FARMERS.Find(id);
            if (fARMER == null)
            {
                return HttpNotFound();
            }
            return View(fARMER);
        }

        // POST: Farmer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FARMER fARMER = db.FARMERS.Find(id);
            db.FARMERS.Remove(fARMER);
            db.SaveChanges();
            TempData["AlertMessage"] = "Farmer Deleted Successfully...!";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
