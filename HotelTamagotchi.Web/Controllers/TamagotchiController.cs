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

namespace HotelTamagotchi.Web.Controllers
{
    public class TamagotchiController : Controller
    {
        private IRepository<Tamagotchi> TamagotchiRepo;
        private IRepository<HotelRoom> HotelRoomRepo;

        public TamagotchiController(IRepository<Tamagotchi> tamagotchiRepository, IRepository<HotelRoom> hotelRoomRepository) : base()
        {
            TamagotchiRepo = tamagotchiRepository;
            HotelRoomRepo = hotelRoomRepository;
        }

        // We need this costructor for something, we not know for what tough.....
        public TamagotchiController()
        {
            TamagotchiRepo = new TamagotchiRepository();
            HotelRoomRepo = new HotelRoomRepository();
        }

        // GET: Tamagotchi
        public ActionResult Index()
        {
            return View(TamagotchiRepo.GetAll());
        }

        // GET: Tamagotchi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tamagotchi tamagotchi = TamagotchiRepo.Find(id);
            if (tamagotchi == null)
            {
                return HttpNotFound();
            }
            return View(tamagotchi);
        }

        // GET: Tamagotchi/Create
        public ActionResult Create()
        {
            ViewBag.HotelRoomId = new SelectList(HotelRoomRepo.GetAll(), "Id", "Id");
            return View();
        }

        // POST: Tamagotchi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HotelRoomId,Name,Age,Pennies,Level,Health,Boredom,Alive")] Tamagotchi tamagotchi)
        {
            if (ModelState.IsValid)
            {
                tamagotchi.Age = 0;
                tamagotchi.Boredom = 0;
                tamagotchi.Health = 100;
                tamagotchi.Level = 0;
                tamagotchi.Pennies = 100;
                TamagotchiRepo.Add(tamagotchi);
                return RedirectToAction("Index");
            }

            ViewBag.HotelRoomId = new SelectList(HotelRoomRepo.GetAll(), "Id", "Id", tamagotchi.HotelRoomId);
            return View(tamagotchi);
        }

        // GET: Tamagotchi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tamagotchi tamagotchi = TamagotchiRepo.Find(id);
            if (tamagotchi == null)
            {
                return HttpNotFound();
            }
            ViewBag.HotelRoomId = new SelectList(HotelRoomRepo.GetAll(), "Id", "Id", tamagotchi.HotelRoomId);
            return View(tamagotchi);
        }

        // POST: Tamagotchi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HotelRoomId,Name,Age,Pennies,Level,Health,Boredom,Alive")] Tamagotchi tamagotchi)
        {
            if (ModelState.IsValid)
            {
                TamagotchiRepo.SetChanged(tamagotchi);
                return RedirectToAction("Index");
            }
            ViewBag.HotelRoomId = new SelectList(HotelRoomRepo.GetAll(), "Id", "Id", tamagotchi.HotelRoomId);
            return View(tamagotchi);
        }

        // GET: Tamagotchi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tamagotchi tamagotchi = TamagotchiRepo.Find(id);
            if (tamagotchi == null)
            {
                return HttpNotFound();
            }
            return View(tamagotchi);
        }

        // POST: Tamagotchi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tamagotchi tamagotchi = TamagotchiRepo.Find(id);
            TamagotchiRepo.Remove(tamagotchi);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                TamagotchiRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
