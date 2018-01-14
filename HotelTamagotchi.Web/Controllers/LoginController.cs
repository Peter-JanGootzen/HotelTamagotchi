using HotelTamagotchi.Web.Repositories;
using HotelTamagotchi.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelTamagotchi.Web.Controllers
{
    public class LoginController : Controller
    {
        UserRepository _userRepository;

        public LoginController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: Login
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            UserViewModel user = _userRepository.Authenticate(username, password);
            if(user.ToModel() == null)
            {
                TempData["WrongCredentials"] = "You have entered wrong credentials, please check them and try again!";
                return RedirectToAction("Login");
            }
            else
            {
                Session["User"] = user.Username;
                Session["Role"] = user.Role;
                Session["Password"] = "Hunter2";
                TempData["LoggedIn"] = "You are logged in with username: " + Session["User"] + " and password: " + Session["Password"]; 
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOut()
        {
            Session["User"] = null;
            Session["Role"] = null;
            Session["Password"] = "Hunter2";
            TempData["Logout"] = "Successfully logged out";
            return RedirectToAction("Index", "Home");
        }
    }
}