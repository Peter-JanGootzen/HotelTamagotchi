using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelTamagotchi.Web;
using HotelTamagotchi.Web.Controllers;
using HotelTamagotchi.Web.Repositories;
using HotelTamagotchi.Web.Models;

namespace HotelTamagotchi.Test.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            IHotelTamagotchiContext c = new FakeHotelTamagotchiContext();
            ITamagotchiRepository tR = new TamagotchiRepository(c);
            IHotelRoomRepository hR = new HotelRoomRepository(c);

            // Arrange
            HomeController controller = new HomeController(hR, tR);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            IHotelTamagotchiContext c = new FakeHotelTamagotchiContext();
            ITamagotchiRepository tR = new TamagotchiRepository(c);
            IHotelRoomRepository hR = new HotelRoomRepository(c);

            // Arrange
            HomeController controller = new HomeController(hR, tR); ;

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Programmeren 6 assessment by Bram-Boris Meerlo and Peter-Jan Gootzen", result.ViewBag.Message);
        }
    }
}
