using System;
using NUnit.Framework;
using HotelTamagotchi.Web.Controllers;
using HotelTamagotchi.Web.Models;
using HotelTamagotchi.Web.Repositories;
using System.Web.Mvc;
using System.Net;
using HotelTamagotchi.Web.ViewModels;
using Moq;

namespace HotelTamagotchi.Test.Controllers
{
    [TestFixture]
    public class TamagotchiControllerTest
    {
        [Test]
        public void Test_Create()
        {
            TamagotchiViewModel t = new TamagotchiViewModel()
            {
                Name = "Test_Creat",
                Alive = false
            };
            IHotelTamagotchiContext c = new FakeHotelTamagotchiContext();
            ITamagotchiRepository tr = new TamagotchiRepository(c);
            TamagotchiController tc = new TamagotchiController(tr);
            var ccMock = new Mock<ControllerContext>();
            ccMock.SetupGet(x => x.HttpContext.Session["User"]).Returns("testUser");
            ccMock.SetupGet(x => x.HttpContext.Session["UserId"]).Returns(1);
            ccMock.SetupGet(x => x.HttpContext.Session["Role"]).Returns(UserRole.Customer);
            tc.ControllerContext = ccMock.Object;

            tc.Create(t);

            Assert.AreEqual(tr.Find(t.Id).ToModel(), t.ToModel());
        }

        [Test]
        public void Test_Edit()
        {
            TamagotchiViewModel t = new TamagotchiViewModel()
            {
                Name = "Test_Edit",
                Alive = false
            };
            IHotelTamagotchiContext c = new FakeHotelTamagotchiContext();
            ITamagotchiRepository tr = new TamagotchiRepository(c);
            TamagotchiController tc = new TamagotchiController(tr);
            var ccMock = new Mock<ControllerContext>();
            ccMock.SetupGet(x => x.HttpContext.Session["User"]).Returns("testUser");
            ccMock.SetupGet(x => x.HttpContext.Session["UserId"]).Returns(1);
            ccMock.SetupGet(x => x.HttpContext.Session["Role"]).Returns(UserRole.Customer);
            tc.ControllerContext = ccMock.Object;

            tc.Create(t);
            t.Health = 20;
            tc.Edit(t);
            Assert.AreEqual(tr.Find(t.Id).Health, 20);
        }

        [Test]
        public void Test_Errors()
        {
            IHotelTamagotchiContext c = new FakeHotelTamagotchiContext();
            ITamagotchiRepository tr = new TamagotchiRepository(c);
            TamagotchiController tc = new TamagotchiController(tr);
            var ccMock = new Mock<ControllerContext>();
            ccMock.SetupGet(x => x.HttpContext.Session["User"]).Returns("testUser");
            ccMock.SetupGet(x => x.HttpContext.Session["UserId"]).Returns(1);
            ccMock.SetupGet(x => x.HttpContext.Session["Role"]).Returns(UserRole.Customer);
            tc.ControllerContext = ccMock.Object;

            var r = tc.Delete(null);
            var r2 = tc.Delete(0);
            var r3 = tc.Edit(0);
            //Untestable because null is ambigious between a int? and an object
            //var r3 = tc.Edit(null);

            Assert.That(r, Is.TypeOf<HttpStatusCodeResult>());
            Assert.That(r2, Is.TypeOf<HttpNotFoundResult>());
            Assert.That(r3, Is.TypeOf<HttpNotFoundResult>());
        }

        [Test]
        public void Test_DeleteConfirmed()
        {
            TamagotchiViewModel t = new TamagotchiViewModel()
            {
                Name = "Test_Remov",
                Alive = false
            };
            IHotelTamagotchiContext c = new FakeHotelTamagotchiContext();
            ITamagotchiRepository tr = new TamagotchiRepository(c); ;
            TamagotchiController tc = new TamagotchiController(tr);
            var ccMock = new Mock<ControllerContext>();
            ccMock.SetupGet(x => x.HttpContext.Session["User"]).Returns("testUser");
            ccMock.SetupGet(x => x.HttpContext.Session["UserId"]).Returns(1);
            ccMock.SetupGet(x => x.HttpContext.Session["Role"]).Returns(UserRole.Customer);
            tc.ControllerContext = ccMock.Object;

            tc.Create(t);
            tc.DeleteConfirmed(t.Id);

            Assert.IsFalse(tr.GetAll().Contains(t));
        }

        [Test]
        public void Test_Dispose()
        {
            IHotelTamagotchiContext c = new FakeHotelTamagotchiContext();
            ITamagotchiRepository tr = new TamagotchiRepository(c);
            TamagotchiController tc = new TamagotchiController(tr);
            var ccMock = new Mock<ControllerContext>();
            ccMock.SetupGet(x => x.HttpContext.Session["User"]).Returns("testUser");
            ccMock.SetupGet(x => x.HttpContext.Session["UserId"]).Returns(1);
            ccMock.SetupGet(x => x.HttpContext.Session["Role"]).Returns(UserRole.Customer);
            tc.ControllerContext = ccMock.Object;

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
