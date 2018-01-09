using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelTamagotchi.Web.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace HotelTamagotchi.Test.Controllers
{
    [TestClass]
    public class HotelRoomTest
    {
        [TestMethod]
        public void Hotelroom_properties()
        {
            //arrange
            HotelRoom h = new HotelRoom() { Size = HotelRoomSize.Bigroom, Type = HotelRoomType.Gameroom };

            //assert
            Assert.AreEqual(HotelRoomSize.Bigroom, h.Size);
            Assert.AreEqual(HotelRoomType.Gameroom, h.Type);
        }

        [TestMethod]
        public void Hotelroom_valid_validation()
        {
            //arrange
            HotelRoom h = new HotelRoom() { Size = HotelRoomSize.Bigroom, Type = HotelRoomType.Gameroom };
            //act
            var errors = h.Validate(null);
            //assert
            Assert.AreEqual(0, errors.Count());
        }
    }
}
