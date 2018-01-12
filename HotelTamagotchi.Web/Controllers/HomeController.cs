using HotelTamagotchi.Web.App_Start;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelTamagotchi.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Programmeren 6 assessment by Bram-Boris Meerlo and Peter-Jan Gootzen";

            return View();
        }

        public ActionResult StartNight()
        {
            NightController nC = NinjectWebCommon.Kernel.Get<NightController>();
            if (nC.StartNight())
                TempData["NightSuccess"] = "The night has been finished, go check in on your tamagotchi or book another room";
            else
                TempData["NightWarning"] = "No rooms are booked at the moment, go book a room for your Tamagotchi!";
            return Redirect(".");
        }
    }
}