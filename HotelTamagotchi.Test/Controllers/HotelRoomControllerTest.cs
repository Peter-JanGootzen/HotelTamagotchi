using System;
using HotelTamagotchi.Web.Controllers;
using NUnit.Framework;
using HotelTamagotchi.Web.Models;
using HotelTamagotchi.Web.Repositories;
using System.Web.Mvc;
using System.Net;
using HotelTamagotchi.Web.ViewModels;
using Moq;

namespace HotelHotelRoom.Test.Controllers
{
    [TestFixture]
    public class HotelRoomControllerTest
    {
        [Test]
        public void Test_Create()
        {
            HotelRoomViewModel t = new HotelRoomViewModel()
            {
                Size = 2,
                Type = HotelRoomType.Workroom
            };

            IHotelTamagotchiContext c = new FakeHotelTamagotchiContext();
            IHotelRoomRepository tr = new HotelRoomRepository(c);
            HotelRoomController tc = new HotelRoomController(tr);
            var ccMock = new Mock<ControllerContext>();
            ccMock.SetupGet(x => x.HttpContext.Session["User"]).Returns("testUser");
            ccMock.SetupGet(x => x.HttpContext.Session["Role"]).Returns(UserRole.Staff);
            tc.ControllerContext = ccMock.Object;

            tc.Create(t);

            Assert.AreEqual(tr.Find(t.Id).ToModel(), t.ToModel());
            tr.Remove(t);
        }

        [Test]
        public void Test_Edit()
        {
            HotelRoomViewModel t = new HotelRoomViewModel()
            {
                Size = 2,
                Type = HotelRoomType.Workroom
            };
            IHotelTamagotchiContext c = new FakeHotelTamagotchiContext();
            IHotelRoomRepository tr = new HotelRoomRepository(c);
            HotelRoomController tc = new HotelRoomController(tr);
            var ccMock = new Mock<ControllerContext>();
            ccMock.SetupGet(x => x.HttpContext.Session["User"]).Returns("testUser");
            ccMock.SetupGet(x => x.HttpContext.Session["Role"]).Returns(UserRole.Staff);
            tc.ControllerContext = ccMock.Object;

            tc.Create(t);
            t.Size = 5;
            tc.Edit(t);
            Assert.AreEqual(tr.Find(t.Id).Size, 5);
            tr.Remove(t);
        }

        [Test]
        public void Test_Errors()
        {
            IHotelTamagotchiContext c = new FakeHotelTamagotchiContext();
            IHotelRoomRepository tr = new HotelRoomRepository(c);
            HotelRoomController tc = new HotelRoomController(tr);
            var ccMock = new Mock<ControllerContext>();
            ccMock.SetupGet(x => x.HttpContext.Session["User"]).Returns("testUser");
            ccMock.SetupGet(x => x.HttpContext.Session["Role"]).Returns(UserRole.Staff);
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
            HotelRoomViewModel t = new HotelRoomViewModel()
            {
                Size = 2,
                Type = HotelRoomType.Workroom
            };
            IHotelTamagotchiContext c = new FakeHotelTamagotchiContext();
            IHotelRoomRepository tr = new HotelRoomRepository(c);
            HotelRoomController tc = new HotelRoomController(tr);
            var ccMock = new Mock<ControllerContext>();
            ccMock.SetupGet(x => x.HttpContext.Session["User"]).Returns("testUser");
            ccMock.SetupGet(x => x.HttpContext.Session["Role"]).Returns(UserRole.Staff);
            tc.ControllerContext = ccMock.Object;

            tc.Create(t);
            tc.DeleteConfirmed(t.Id);

            Assert.IsFalse(tr.GetAll().Contains(t));
        }

        [Test]
        public void Test_Dispose()
        {
            IHotelTamagotchiContext c = new FakeHotelTamagotchiContext();
            IHotelRoomRepository tr = new HotelRoomRepository(c);
            HotelRoomController tc = new HotelRoomController(tr);
            var ccMock = new Mock<ControllerContext>();
            ccMock.SetupGet(x => x.HttpContext.Session["User"]).Returns("testUser");
            ccMock.SetupGet(x => x.HttpContext.Session["Role"]).Returns(UserRole.Staff);
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
