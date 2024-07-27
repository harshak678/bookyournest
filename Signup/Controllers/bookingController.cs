using Signup.Models;
using Signup.Repository;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Signup.Controllers
{
    public class bookingController : Controller
    {
        private bookingrepo bookingRepository;

        public bookingController()
        {
            bookingRepository = new bookingrepo();
        }

        // GET: booking
        public ActionResult Index()
        {
            return View();
        }

        // GET: booking/bookingDetails
        public ActionResult bookingDetails()
        {
            // Retrieve all booking details
            List<userbooking> bookings = bookingRepository.GetbookingDetails();
            return View(bookings);
        }

        // GET: booking/bookingDetailsforuser
        public ActionResult bookingDetailsforuser()
        {
            var loggedInEmailAddress = Session["id"] as string;

            if (!string.IsNullOrEmpty(loggedInEmailAddress))
            {
                // Retrieve booking details for the logged-in user
                List<userbooking> bookings = bookingRepository.GetBookingDetailsForUser(loggedInEmailAddress);
                return View(bookings);
            }
            else
            {
                // Redirect to login if not logged in
                return RedirectToAction("Login", "Signup");
            }
        }

        // GET: booking/roombooking
        public ActionResult roombooking()
        {
            return View();
        }

        // POST: booking/roombooking
        [HttpPost]
        public ActionResult roombooking(userbooking ub)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert booking details into the database
                    bookingRepository.InsertBooking(ub);
                    TempData["bookmessage"] = "Your booking details have been received.";
                    return RedirectToAction("roombooking");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error occurred: " + ex.Message);
                }
            }
            return View(ub);
        }

        // GET: booking/Edit/5
        public ActionResult Edit(int id)
        {
            // Placeholder action for editing a booking
            return View();
        }

        // POST: booking/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Implement update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: booking/Delete/5
        public ActionResult Delete(int id)
        {
            // Placeholder action for deleting a booking
            return View();
        }

        // POST: booking/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Implement delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: booking/logout
        public ActionResult logout()
        {
            // Clear session and redirect to login page
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("login", "signup");
        }
    }
}

