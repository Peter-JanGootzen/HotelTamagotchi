using System;
using NUnit.Framework;
using HotelTamagotchi.Web.Controllers;
using HotelTamagotchi.Web.Models;
using HotelTamagotchi.Web.Repositories;
using HotelTamagotchi.Web.ViewModels;
using System.Web.Mvc;
using System.Web;
using Moq;

namespace HotelTamagotchi.Test.Controllers
{
    [TestFixture]
    public class BookingControllerTest
    {
        [Test]
        public void BookingController_CreateBooking()
        {
            IHotelTamagotchiContext c = new FakeHotelTamagotchiContext();
            IHotelRoomRepository hr = new HotelRoomRepository(c);
            ITamagotchiRepository tr = new TamagotchiRepository(c);
            BookingController bc = new BookingController(hr, tr);
            var ccMock = new Mock<ControllerContext>();
            ccMock.SetupGet(x => x.HttpContext.Session["User"]).Returns("testUser");
            ccMock.SetupGet(x => x.HttpContext.Session["Role"]).Returns(UserRole.Customer);
            bc.ControllerContext = ccMock.Object;
            FormCollection fc = new FormCollection();


            TamagotchiViewModel t = new TamagotchiViewModel()
            {
                Name = "Test",
                Alive = true

            };
            tr.Add(t);
            HotelRoomViewModel h = new HotelRoomViewModel()
            {
                Size = 5,
                Type = HotelRoomType.Fightroom
            };
            hr.Add(h);
            fc.Add("1", "true,false");
            bc.Create(fc, h);
            Assert.AreEqual(t.ToModel().HotelRoomId, tr.Find(t.Id).HotelRoomId);
            tr.Remove(t);
            hr.Remove(h);
        }
    }
}
