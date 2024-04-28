using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Farmers_App.Models;
using BCrypt.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http;

namespace Farmers_App.Controllers
{
    public class AdminController : Controller
    {
        private AdminEntities db = new AdminEntities();

        // GET: Admin
        public ActionResult Index()
        {
            return View(db.admins.ToList());
        }

       

        // GET: Admin/Create
        public ActionResult Signup()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup([Bind(Include = "userId,email,password")] admin admin)
        {
            try
            {

                // Check if the email already exists in the database
                if (db.admins.Any(x => x.email == admin.email)) 
                {
                    // If the email already exists, display a notification and return the view
                    ViewBag.Notification = "This account already exists";
                    return View();
                }


                if (ModelState.IsValid)
                {
                    // Hash the password before storing it in the database
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(admin.password);
                    admin.password = hashedPassword;

                    // Add the admin to the database and save changes
                    db.admins.Add(admin);
                    TempData["AlertMessage"] = "Admin Created Successfully...!";
                    db.SaveChanges();

                   
                    // Redirect to the index action of the Admin controller
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                // Log the exception or handle it appropriately
                ViewBag.Notification = "An error occurred while creating the admin account.";
                // Redirect to an error page or display a generic error message
                return View();

            }

            // If ModelState is not valid, return the view with the admin object
            return View(admin);
        }

        // Logout
        public ActionResult Logout()
        {
            try
            {
                // Clear session variables
                Session.Clear();
                // Redirect to the home page
                return RedirectToAction("Login", "Admin");
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Admin");

            }
           
        }

        // login 
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(admin admin)
        {
            // reCaptcha Logic

            // Get reCAPTCHA response from the request
            string gRecaptchaResponse = Request.Form["g-recaptcha-response"];

            // Validate reCAPTCHA
            var response = ValidateRecaptcha(gRecaptchaResponse);
            if (!response.Success)
            {
                ModelState.AddModelError("", "Please complete the reCAPTCHA.");
                return View(admin);
            }



            // login logic 
            try
            {
                // Retrieve the user from the database based on the email
                var user = db.admins.FirstOrDefault(x => x.email == admin.email);

                // If user exists
                if (user != null)
                {
                    // Compare hashed password with user input
                    if (BCrypt.Net.BCrypt.Verify(admin.password, user.password))
                    {
                        // If password is correct, store user ID and email in session variables
                        Session["UserID"] = admin.userId.ToString();
                        Session["Email"] = admin.email.ToString();
                        // Redirect to the index action of the home controller
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            catch (Exception)
            {
                // Handle any exceptions that occur during login
                ViewBag.Notification = "An error occurred while logging in.";
                return View();
            }

            // If login is unsuccessful, display an error message
            //ViewBag.Notification = "Wrong Email or password";
            return View();
            //end
        }

        // Method to validate reCAPTCHA response
        private RecaptchaResponse ValidateRecaptcha(string gRecaptchaResponse)
        {
            var secretKey = "6Ld4rcYpAAAAAEcvmZu4czN5mPB8D62u-zfnoiTL";
            var client = new System.Net.WebClient();
            var response = client.DownloadString($"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={gRecaptchaResponse}");
            var recaptchaResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<RecaptchaResponse>(response);
            return recaptchaResponse;
        }

        // Class to represent the response from the reCAPTCHA verification
        public class RecaptchaResponse
        {
            public bool Success { get; set; }
            public string ChallengeTs { get; set; }
            public string Hostname { get; set; }
            public List<string> ErrorCodes { get; set; }
        }





        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin admin = db.admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userId,email,password")] admin admin)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(admin.password);
                admin.password = hashedPassword;

                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                TempData["AlertMessage"] = "Admin details Updated Successfully...!";
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin admin = db.admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            admin admin = db.admins.Find(id);
            db.admins.Remove(admin);
            db.SaveChanges();
            TempData["AlertMessage"] = "Admin Deleted Successfully...!";
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
