using HotelTamagotchi.Web.App_Start;
using HotelTamagotchi.Web.Repositories;
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

        private IHotelRoomRepository _hotelRoomRepository;
        private ITamagotchiRepository _tamagotchiRepository;

        public HomeController(IHotelRoomRepository hotelRoomRepository, ITamagotchiRepository tamagotchiRepository)
        {
            _hotelRoomRepository = hotelRoomRepository;
            _tamagotchiRepository = tamagotchiRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Programmeren 6 assessment by Bram-Boris Meerlo and Peter-Jan Gootzen";

            return View();
        }

        public ActionResult Book()
        {
            ITamagotchiRepository TamagotchiRepo = NinjectWebCommon.Kernel.Get<TamagotchiRepository>();
            IHotelRoomRepository HotelRoomRepo = NinjectWebCommon.Kernel.Get<HotelRoomRepository>();
            if (TamagotchiRepo.GetAllHomelessTamagotchi().Count == 0)
            {
                TempData["TamagotchiCount"] = "There are no tamagotchis to book! Remove tamagotchis from a hotelroom or start the night!";
                return RedirectToAction("Index");
            }
            if(HotelRoomRepo.GetAllAvailableHotelRooms().Count == 0)
            {
                TempData["HotelRoomCount"] = "There are no hotelrooms available to book! Remove tamagotchis from a hotelroom or start the night!";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Booking");
        }

        public ActionResult StartNight()
        {
            NightController nC = new NightController(_tamagotchiRepository, _hotelRoomRepository);
            TempData["NightSuccess"] = "The night has been finished, go check in on your tamagotchi or book another room";
            return Redirect(".");
        }
    }
}