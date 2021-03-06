﻿using HotelTamagotchi.Web.Models;
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
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                TempData["NotLoggedIn"] = "Please login or register to continue!";
                return RedirectToAction("Index", "Home");
            }
            if (HotelRoomRepo.GetAllAvailableHotelRooms().Count == 0)
            {
                TempData["HotelRoomCount"] = "There are no hotelrooms available to book! Remove tamagotchis from a hotelroom or start the night!";
                return RedirectToAction("Index", "Home");
            }
            return View(HotelRoomRepo.GetAll());
        }
        [HttpGet]
        public ActionResult Create(int? id)
        {
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }
            if (!Session["Role"].Equals(UserRole.Customer))
            {
                TempData["NotAuthorized"] = "You are not authorized to perform these actions!";
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelRoomViewModel hotelRoom = HotelRoomRepo.Find(id);
            if (hotelRoom == null)
            {
                return HttpNotFound();
            }
            return View(Tuple.Create(hotelRoom, TamagotchiRepo.GetAllHomelessTamagotchi()));
        }
        [HttpPost]
        public ActionResult Create(FormCollection formCollection, HotelRoomViewModel hotelroom)
        {
            if (!Session["Role"].Equals(UserRole.Customer))
            {
                TempData["NotAuthorized"] = "You are not authorized to perform these actions!";
                return RedirectToAction("Index", "Home");
            }
            hotelroom = HotelRoomRepo.Find(hotelroom.Id);
            List<TamagotchiViewModel> addToHotel = new List<TamagotchiViewModel>();
            int i = 0;
            foreach(var t in TamagotchiRepo.GetAllHomelessTamagotchi())
            {
                if(t.HotelRoomId != null)
                {
                    ModelState.AddModelError(string.Empty, "You cannot book a tamagotchi that already has a room");
                    TempData["ViewData"] = ViewData;
                    return RedirectToAction("Create");
                }
                if (i > (int)hotelroom.Size)
                {
                    string sizeError = "You may only book " + (int)hotelroom.Size + " tamagotchis";
                    ModelState.AddModelError(string.Empty,sizeError);
                    TempData["ViewData"] = ViewData;
                    return RedirectToAction("Create");

                }
                if (formCollection[t.Id +""] != null && formCollection[t.Id + ""].Equals("true,false"))
                {
                    addToHotel.Add(t);
                    i++;
                }
            }
            if(addToHotel.Count == 0)
            {
                ModelState.AddModelError(String.Empty, "You have to book atleast 1 tamagotchi");
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Create");
            }
            foreach(var t in addToHotel)
            {
                t.HotelRoomId = hotelroom.Id;
                TamagotchiRepo.SetChanged(t);
            }
            TempData["Success"] = "Succesfully created booking for hotelroom number: " + hotelroom.Id;
            return RedirectToAction("Index");
        }
    }
}