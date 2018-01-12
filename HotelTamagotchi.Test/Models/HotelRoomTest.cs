﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelTamagotchi.Web.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using HotelTamagotchi.Web.ViewModels;

namespace HotelTamagotchi.Test.Models
{
    [TestClass]
    public class HotelRoomTest
    {
        [TestMethod]
        public void Hotelroom_properties()
        {
            //arrange
            HotelRoomViewModel h = new HotelRoomViewModel() { Size = 5, Type = HotelRoomType.Gameroom };

            //assert
            Assert.AreEqual(5, h.Size);
            Assert.AreEqual(HotelRoomType.Gameroom, h.Type);
        }

        [TestMethod]
        public void Hotelroom_valid_validation()
        {
            //arrange
            HotelRoomViewModel h = new HotelRoomViewModel() { Size = 5, Type = HotelRoomType.Gameroom };
            //act
            var errors = h.Validate(null);
            //assert
            Assert.AreEqual(0, errors.Count());
        }
    }
}
