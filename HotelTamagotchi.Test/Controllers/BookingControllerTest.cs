using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelTamagotchi.Web.Controllers;
using HotelTamagotchi.Web.Models;
using HotelTamagotchi.Web.Repositories;
using HotelTamagotchi.Web.ViewModels;
using System.Web.Mvc;

namespace HotelTamagotchi.Test.Controllers
{
    [TestClass]
    public class BookingControllerTest
    {
        [TestMethod]
        public void BookingController_CreateBooking()
        {
            HotelTamagotchiEntities d = new HotelTamagotchiEntities();
            IHotelRoomRepository hr = new HotelRoomRepository(d);
            ITamagotchiRepository tr = new TamagotchiRepository(d);
            BookingController bc = new BookingController(hr, tr);
            FormCollection fc = new FormCollection();
            TamagotchiViewModel t = new TamagotchiViewModel()
            {
                Name = "Test",
                Pennies = 100,
                Age = 12,
                Level = 100,
                Health = 4,
                Boredom = 12,
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
