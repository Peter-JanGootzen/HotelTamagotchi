using HotelTamagotchi.Web.Models;
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
        IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: Login
        public ActionResult Register()
        {
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }
            return View();
        }
        [HttpPost]
        public ActionResult Register(string username, string password, UserRole role)
        {
            if(!_userRepository.Exists(username))
            {
                ModelState.AddModelError(String.Empty, "This username already exists!");
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Register");
            }
            if(role != UserRole.Customer & role != UserRole.Staff)
            {
                ModelState.AddModelError(String.Empty, "Please select customer or role as type");
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Register");
            }
            else
            {
                UserViewModel user = new UserViewModel() { Username = username, Password = password, Role = role };
                _userRepository.Add(user);
                Session["UserId"] = user.Id;
                Session["User"] = user.Username;
                Session["Role"] = user.Role;
                Session["Password"] = "Hunter2";
                TempData["LoggedIn"] = "Successfully registered, you are now logged in!";
                return RedirectToAction("Index", "Home");
            }
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
                Session["UserId"] = user.Id;
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