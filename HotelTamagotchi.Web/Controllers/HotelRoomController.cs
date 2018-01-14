using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelTamagotchi.Web.Models;
using HotelTamagotchi.Web.Repositories;
using HotelTamagotchi.Web.ViewModels;

namespace HotelTamagotchi.Web.Controllers
{
    public class HotelRoomController : Controller
    {
        private IHotelRoomRepository HotelRoomRepo;

        public HotelRoomController(IHotelRoomRepository hotelRoomRepository)
        {
            HotelRoomRepo = hotelRoomRepository;
        }

        // GET: HotelRoom
        public ActionResult Index()
        {
            return View(HotelRoomRepo.GetAll());
        }

        // GET: HotelRoom/Details/5
        public ActionResult Details(int? id)
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
            return View(hotelRoom);
        }

        // GET: HotelRoom/Create
        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                TempData["NotLoggedIn"] = "Please login or register to continue!";
                return RedirectToAction("Index", "Home");
            }
            if (!Session["Role"].Equals(UserRole.Staff))
            {
                TempData["NotAuthorized"] = "You are not authorized to perform these actions!";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: HotelRoom/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Size,Type")] HotelRoomViewModel hotelRoom)
        {
            if (Session["User"] == null)
            {
                TempData["NotLoggedIn"] = "Please login or register to continue!";
                return RedirectToAction("Index", "Home");
            }
            if (!Session["Role"].Equals(UserRole.Staff))
            {
                TempData["NotAuthorized"] = "You are not authorized to perform these actions!";
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                HotelRoomRepo.Add(hotelRoom);
                return RedirectToAction("Index");
            }

            return View(hotelRoom);
        }

        // GET: HotelRoom/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["User"] == null)
            {
                TempData["NotLoggedIn"] = "Please login or register to continue!";
                return RedirectToAction("Index", "Home");
            }
            if (!Session["Role"].Equals(UserRole.Staff))
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

            var sizes = new List<int>
            {
                2,
                3,
                5
            };
            ViewBag.SelectList = new SelectList(sizes);
            return View(hotelRoom);
        }

        // POST: HotelRoom/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Size,Type")] HotelRoomViewModel hotelRoom)
        {
            if (Session["User"] == null)
            {
                TempData["NotLoggedIn"] = "Please login or register to continue!";
                return RedirectToAction("Index", "Home");
            }
            if (!Session["Role"].Equals(UserRole.Staff))
            {
                TempData["NotAuthorized"] = "You are not authorized to perform these actions!";
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                HotelRoomRepo.SetChanged(hotelRoom);
                return RedirectToAction("Index");
            }
            return View(hotelRoom);
        }

        // GET: HotelRoom/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["User"] == null)
            {
                TempData["NotLoggedIn"] = "Please login or register to continue!";
                return RedirectToAction("Index", "Home");
            }
            if (!Session["Role"].Equals(UserRole.Staff))
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
            return View(hotelRoom);
        }

        // POST: HotelRoom/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["User"] == null)
            {
                TempData["NotLoggedIn"] = "Please login or register to continue!";
                return RedirectToAction("Index", "Home");
            }
            if (!Session["Role"].Equals(UserRole.Staff))
            {
                TempData["NotAuthorized"] = "You are not authorized to perform these actions!";
                return RedirectToAction("Index", "Home");
            }
            HotelRoomViewModel hotelRoom = HotelRoomRepo.Find(id);
            HotelRoomRepo.Remove(hotelRoom);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                HotelRoomRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
