using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelTamagotchi.Web.Models;

namespace HotelTamagotchi.Web.Controllers
{
    public class TamagotchiController : Controller
    {
        private HotelTamagotchiEntities db = new HotelTamagotchiEntities();

        // GET: Tamagotchi
        public ActionResult Index()
        {
            var tamagotchis = db.Tamagotchis.Include(t => t.HotelRoom);
            return View(tamagotchis.ToList());
        }

        // GET: Tamagotchi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tamagotchi tamagotchi = db.Tamagotchis.Find(id);
            if (tamagotchi == null)
            {
                return HttpNotFound();
            }
            return View(tamagotchi);
        }

        // GET: Tamagotchi/Create
        public ActionResult Create()
        {
            ViewBag.HotelRoomId = new SelectList(db.HotelRooms, "Id", "Id");
            return View();
        }

        // POST: Tamagotchi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HotelRoomId,Name,Pennies,Level,Health,Boredom,Alive")] Tamagotchi tamagotchi)
        {
            if (ModelState.IsValid)
            {
                db.Tamagotchis.Add(tamagotchi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HotelRoomId = new SelectList(db.HotelRooms, "Id", "Id", tamagotchi.HotelRoomId);
            return View(tamagotchi);
        }

        // GET: Tamagotchi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tamagotchi tamagotchi = db.Tamagotchis.Find(id);
            if (tamagotchi == null)
            {
                return HttpNotFound();
            }
            ViewBag.HotelRoomId = new SelectList(db.HotelRooms, "Id", "Id", tamagotchi.HotelRoomId);
            return View(tamagotchi);
        }

        // POST: Tamagotchi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HotelRoomId,Name,Pennies,Level,Health,Boredom,Alive")] Tamagotchi tamagotchi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tamagotchi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HotelRoomId = new SelectList(db.HotelRooms, "Id", "Id", tamagotchi.HotelRoomId);
            return View(tamagotchi);
        }

        // GET: Tamagotchi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tamagotchi tamagotchi = db.Tamagotchis.Find(id);
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
            Tamagotchi tamagotchi = db.Tamagotchis.Find(id);
            db.Tamagotchis.Remove(tamagotchi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
