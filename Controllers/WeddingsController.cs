using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace WeddingPlanner.Controllers{
    public class WeddingsController : Controller{
        private WeddingPlannerContext _context;
        public WeddingsController(WeddingPlannerContext context){
            _context = context;
        }
        [HttpGet]
        [Route("/Dashboard")]
        public IActionResult DisplayDashboard(){
            int? UserId = HttpContext.Session.GetInt32("userid");
            ViewBag.Userid = UserId;
            if (UserId == null){
                return RedirectToAction("Index", "Home");
            }
            List<Wedding> AllWeddings = _context.Weddings.Include(Wedding=>Wedding.guests).ThenInclude(Guest=>Guest.User).ToList();
            ViewBag.Weddings = AllWeddings;
            return View("Dashboard");
        }
        [HttpGet]
        [Route("/CreateWedding")]
        public IActionResult CreateWedding(){
            int? UserId = HttpContext.Session.GetInt32("userid");
            if (UserId == null){
                return RedirectToAction("Index", "Home");
            }
            return View("CreateWedding");
        }
        [HttpPost]
        [Route("/viewguest")]
        public IActionResult ViewWedding(int weddingid){
            return RedirectToAction("DisplayGuestList", new {WeddingID=weddingid});
        }

        [HttpGet]
        [Route("/guestlist/{WeddingID}")]
        public IActionResult DisplayGuestList(int WeddingID){
            int? UserId = HttpContext.Session.GetInt32("userid");
            Console.WriteLine(WeddingID);
            Wedding Wed = _context.Weddings.SingleOrDefault(Wedding=> Wedding.WeddingId == WeddingID);
            Console.WriteLine("Wedding query", Wed);
            ViewBag.Wed = Wed;
            List<Guest> Allguests = _context.Guests.Include(Guest=>Guest.User).Include(Guest=>Guest.Wedding).Where(Guest=>Guest.WeddingId == WeddingID).ToList();
            ViewBag.Guests = Allguests;
            return View("GuestList");
        }

        [HttpPost]
        [Route("/addwedding")]
        public IActionResult AddWedding(CreateWeddingViewModel model, Wedding NewWedding){
            if(ModelState.IsValid){
                if(model.date < DateTime.Today){
                    TempData["InvalidDate"] = "Wedding Date Must be in the Future";
                    return View("CreateWedding");
                }
                int? UserId = HttpContext.Session.GetInt32("userid");
                List<Wedding> AllWeddings = _context.Weddings.Include(Wedding=>Wedding.User).Where(Wedding=>Wedding.UserId == UserId).ToList();
                if(AllWeddings.Count > 0){
                    TempData["weddingcreated"] = "You have already created a Wedding for yourself.";
                }
                NewWedding.UserId = (int)UserId;
                _context.Weddings.Add(NewWedding);
                _context.SaveChanges();
                Wedding Wed = _context.Weddings.SingleOrDefault(Wedding=> Wedding.wedderone == NewWedding.wedderone);
                HttpContext.Session.SetInt32("weddingid",(int)Wed.WeddingId);
                int? WeddingId = HttpContext.Session.GetInt32("weddingid");
                return RedirectToAction("DisplayGuestList", new{WeddingID = WeddingId});
            }
            return View("CreateWedding");
        }
        [HttpPost]
        [Route("/deletewedding")]
        public IActionResult DeleteWedding(){
            int? UserId = HttpContext.Session.GetInt32("userid");
            Wedding Yourwedding = _context.Weddings.SingleOrDefault(Wedding=>Wedding.UserId == UserId);
            _context.Weddings.Remove(Yourwedding);
            _context.SaveChanges();
            return RedirectToAction("DisplayDashboard");
        }
        [HttpPost]
        [Route("/rsvp")]
        public IActionResult Rsvp(int weddingid){
            int? UserId = HttpContext.Session.GetInt32("userid");
            Guest NewGuest = new Guest{
                UserId = (int)UserId,
                WeddingId = weddingid,
            };
            _context.Guests.Add(NewGuest);
            _context.SaveChanges();
            return RedirectToAction("DisplayDashboard");
        }
        [HttpPost]
        [Route("/unrsvp")]
        public IActionResult Unrsvp(int weddingid){
            int? UserId = HttpContext.Session.GetInt32("userid");
            Guest Deleteguest = _context.Guests.SingleOrDefault(Guest=>Guest.UserId == UserId);
            _context.Guests.Remove(Deleteguest);
            _context.SaveChanges();
            return RedirectToAction("DisplayDashboard");
        }
    }
}