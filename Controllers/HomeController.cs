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

namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private WeddingPlannerContext _context;
        public HomeController(WeddingPlannerContext context){
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }

        [HttpPost]
        [Route("/register")]
        public IActionResult Register(RegisterViewModel model, User NewUser){
            if(ModelState.IsValid){
                List<User> AllUsers = _context.Users.Where(User=>User.email == model.email).ToList();
                if(AllUsers.Count > 0){
                    TempData["emailused"] = "Email is already un use. Please use another.";
                    return View("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                NewUser.password = Hasher.HashPassword(NewUser, NewUser.password);
                _context.Users.Add(NewUser);
                _context.SaveChanges();
                User Reg = _context.Users.SingleOrDefault(User=> User.email == NewUser.email);
                HttpContext.Session.SetInt32("userid", (int)Reg.UserId);
                return RedirectToAction("DisplayDashboard", "Weddings");
            }
            return View("Index");
        }
        [HttpPost]
        [Route("/login")]
        public IActionResult Login(string loginemail, string loginpassword){
            User Login = _context.Users.SingleOrDefault(user => user.email == loginemail);
            if(Login == null){
                TempData["Invalidemail"] = "Email not Registered. Have you Registered?";
                return View("Index");
            }
            if(Login != null && loginpassword != null){
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(Login, Login.password, loginpassword)){
                    HttpContext.Session.SetInt32("userid", (int)Login.UserId);
                    return RedirectToAction("DisplayDashboard", "Weddings");
                }
            }
            TempData["InvalidPW"] = "Invalid Password";
            return View("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
