using HotelTamagotchi.Web.Repositories;
using HotelTamagotchi.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HotelTamagotchi.Web.Controllers
{
    public class BookingController : Controller
    {
        public IHotelRoomRepository HotelRoomRepo { get; private set; }
        public ITamagotchiRepository TamagotchiRepo { get; private set; }

        public BookingController(IHotelRoomRepository hotelRoomRepository, ITamagotchiRepository tamagotchiRepository)
        {
            HotelRoomRepo = hotelRoomRepository;
            TamagotchiRepo = tamagotchiRepository;
        }


        // GET: Booking
        public ActionResult Index()
        {
            return View(HotelRoomRepo.GetAll());
        }

        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelRoomViewModel hotelRoom = HotelRoomRepo.Find(id);
            if (hotelRoom == null)
            {
                return HttpNotFound();
            }
            return View(Tuple.Create(hotelRoom, TamagotchiRepo.GetAll()));
        }
    }
}