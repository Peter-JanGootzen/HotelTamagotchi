using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelTamagotchi.Web.Controllers;
using HotelTamagotchi.Web.Models;
using HotelTamagotchi.Web.Repositories;
using System.Web.Mvc;
using System.Net;
using HotelTamagotchi.Web.ViewModels;

namespace HotelTamagotchi.Test.Controllers
{
    [TestClass]
    public class TamagotchiControllerTest
    {
        [TestMethod]
        public void Test_Create()
        {
            TamagotchiViewModel t = new TamagotchiViewModel()
            {
                Name = "Test_Creat",
                Alive = false
            };
            HotelTamagotchiEntities d = new HotelTamagotchiEntities();
            ITamagotchiRepository tr = new TamagotchiRepository(d);
            TamagotchiController tc = new TamagotchiController(tr);

            tc.Create(t);

            Assert.AreEqual(tr.Find(t.Id).ToModel(), t.ToModel());
        }

        [TestMethod]
        public void Test_Edit()
        {
            TamagotchiViewModel t = new TamagotchiViewModel()
            {
                Name = "Test_Edit",
                Alive = false
            };
            HotelTamagotchiEntities d = new HotelTamagotchiEntities();
            ITamagotchiRepository tr = new TamagotchiRepository(d);
            TamagotchiController tc = new TamagotchiController(tr);

            tc.Create(t);
            t.Health = 20;
            tc.Edit(t);
            Assert.AreEqual(tr.Find(t.Id).Health, 20);
        }

        [TestMethod]
        public void Test_Errors()
        {
            HotelTamagotchiEntities d = new HotelTamagotchiEntities();
            ITamagotchiRepository tr = new TamagotchiRepository(d);
            TamagotchiController tc = new TamagotchiController(tr);

            var r = tc.Delete(null);
            var r2 = tc.Delete(0);
            var r3 = tc.Edit(0);
            //Untestable because null is ambigious between a int? and an object
            //var r3 = tc.Edit(null);

            Assert.IsInstanceOfType(r, typeof(HttpStatusCodeResult)); 
            Assert.IsInstanceOfType(r2, typeof(HttpNotFoundResult));
            Assert.IsInstanceOfType(r3, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void Test_DeleteConfirmed()
        {
            TamagotchiViewModel t = new TamagotchiViewModel()
            {
                Name = "Test_Remov",
                Alive = false
            };
            HotelTamagotchiEntities d = new HotelTamagotchiEntities();
            ITamagotchiRepository tr = new TamagotchiRepository(d);
            TamagotchiController tc = new TamagotchiController(tr);

            tc.Create(t);
            tc.DeleteConfirmed(t.Id);

            Assert.IsFalse(tr.GetAll().Contains(t));
        }

        [TestMethod]
        public void Test_Dispose()
        {
            HotelTamagotchiEntities d = new HotelTamagotchiEntities();
            ITamagotchiRepository tr = new TamagotchiRepository(d);
            TamagotchiController tc = new TamagotchiController(tr);

            tc.Dispose();
            InvalidOperationException e = new InvalidOperationException("Wrong exception");
            try
            {
                tc.Details(1);
            }
            catch (InvalidOperationException exception)
            {
                e = exception;
            }
            Assert.AreEqual(e.Message, "The operation cannot be completed because the DbContext has been disposed.");
        }
    }
}
