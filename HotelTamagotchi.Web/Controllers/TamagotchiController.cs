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
    public class TamagotchiController : Controller
    {
        private ITamagotchiRepository TamagotchiRepo;

        public TamagotchiController(ITamagotchiRepository tamagotchiRepository) : base()
        {
            TamagotchiRepo = tamagotchiRepository;
        }

        // We need this costructor for something, we not know for what tough.....

        // GET: Tamagotchi
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                TempData["NotLoggedIn"] = "Please login or register to continue!";
                return RedirectToAction("Index", "Home");
            }
            return View(TamagotchiRepo.GetAllFromUser((int)Session["UserId"]));
        }

        // GET: Tamagotchi/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["User"] == null)
            {
                TempData["NotLoggedIn"] = "Please login or register to continue!";
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TamagotchiViewModel tamagotchi = TamagotchiRepo.Find(id);
            if (tamagotchi == null)
            {
                return HttpNotFound();
            }
            return View(tamagotchi);
        }

        // GET: Tamagotchi/Create
        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                TempData["NotLoggedIn"] = "Please login or register to continue!";
                return RedirectToAction("Index", "Home");
            }
            else if (!Session["Role"].Equals(UserRole.Customer))
            {
                TempData["NotAuthorized"] = "You are not authorized to perform these actions!";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Tamagotchi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HotelRoomId,Name,Age,Pennies,Level,Health,Boredom,Alive")] TamagotchiViewModel tamagotchi)
        {
            if (Session["User"] == null)
            {
                TempData["NotLoggedIn"] = "Please login or register to continue!";
                return RedirectToAction("Index", "Home");
            }
            else if (!Session["Role"].Equals(UserRole.Customer))
            {
                TempData["NotAuthorized"] = "You are not authorized to perform these actions!";
                return RedirectToAction("Index", "Home");
            }

            tamagotchi.UserId = (int)Session["UserId"];
            if (ModelState.IsValid)
            {
                TamagotchiRepo.Add(tamagotchi);
                return RedirectToAction("Index");
            }

            return View(tamagotchi);
        }

        // GET: Tamagotchi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["User"] == null)
            {
                TempData["NotLoggedIn"] = "Please login or register to continue!";
                return RedirectToAction("Index", "Home");
            }
            if (!Session["Role"].Equals(UserRole.Customer) || !Session["User"].Equals(TamagotchiRepo.Find(id).User.Username))
            {
                TempData["NotAuthorized"] = "You are not authorized to perform these actions!";
                return RedirectToAction("Index", "Tamagotchi");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TamagotchiViewModel tamagotchi = TamagotchiRepo.Find(id);
            if (tamagotchi == null)
            {
                return HttpNotFound();
            }
            return View(tamagotchi);
        }

        // POST: Tamagotchi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TamagotchiViewModel tamagotchi)
        {
            if (Session["User"] == null)
            {
                TempData["NotLoggedIn"] = "Please login or register to continue!";
                return RedirectToAction("Index", "Home");
            }
            if (!Session["Role"].Equals(UserRole.Customer))
            {
                TempData["NotAuthorized"] = "You are not authorized to perform these actions!";
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                TamagotchiRepo.SetChanged(tamagotchi);
                return RedirectToAction("Index");
            }
            return View(tamagotchi);
        }

        // GET: Tamagotchi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["User"] == null)
            {
                TempData["NotLoggedIn"] = "Please login or register to continue!";
                return RedirectToAction("Index", "Home");
            }
            if (!Session["Role"].Equals(UserRole.Customer) || !Session["User"].Equals(TamagotchiRepo.Find(id).User.Username))
            {
                TempData["NotAuthorized"] = "You are not authorized to perform these actions!";
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TamagotchiViewModel tamagotchi = TamagotchiRepo.Find(id);
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
            if (Session["User"] == null)
            {
                TempData["NotLoggedIn"] = "Please login or register to continue!";
                return RedirectToAction("Index", "Home");
            }
            if (!Session["Role"].Equals(UserRole.Customer) || !Session["User"].Equals(TamagotchiRepo.Find(id).User.Username))
            {
                TempData["NotAuthorized"] = "You are not authorized to perform these actions!";
                return RedirectToAction("Index", "Home");
            }
            TamagotchiViewModel tamagotchi = TamagotchiRepo.Find(id);
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
