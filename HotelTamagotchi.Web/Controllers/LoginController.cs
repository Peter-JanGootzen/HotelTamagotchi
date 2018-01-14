using HotelTamagotchi.Web.Repositories;
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
        public ActionResult Index()
        {
            return View();
        }
    }
}