using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelTamagotchi.Web.Controllers;
using HotelTamagotchi.Web.Models;
using HotelTamagotchi.Web.Repositories;

namespace HotelTamagotchi.Test.Controllers
{
    [TestClass]
    public class BookingControllerTest
    {
        [TestMethod]
        public void BookingController_Edit()
        {
            HotelTamagotchiEntities d = new HotelTamagotchiEntities();
            IHotelRoomRepository hr = new HotelRoomRepository(d);
            ITamagotchiRepository tr = new TamagotchiRepository(d);
            BookingController bc = new BookingController(hr, tr);
            Tamagotchi t = new Tamagotchi()
            {
                Id = 1
            };
            
        }
    }
}
