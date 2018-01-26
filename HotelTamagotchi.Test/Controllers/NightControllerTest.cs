using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using HotelTamagotchi.Web.Models;
using HotelTamagotchi.Web.Repositories;
using HotelTamagotchi.Web.Controllers;
using HotelTamagotchi.Web.ViewModels;

namespace HotelTamagotchi.Test.Controllers
{
    [TestFixture]
    public class NightControllerTest
    {
        [Test]
        public void StartNight_Gameroom()
        {
            IHotelTamagotchiContext c = new FakeHotelTamagotchiContext();
            IHotelRoomRepository hR = new HotelRoomRepository(c);
            ITamagotchiRepository tR = new TamagotchiRepository(c);

            HotelRoomViewModel h = new HotelRoomViewModel()
            {
                Size = 5,
                Type = HotelRoomType.Gameroom,
            };
            hR.Add(h);

            TamagotchiViewModel t = new TamagotchiViewModel()
            {
                Name = "Test",
                Alive = true,
                HotelRoom = h,
                HotelRoomId = h.Id,
                Boredom = 50
            };
            tR.Add(t);

            NightController nC = new NightController(tR, hR);
            nC.StartNight();

            Assert.IsTrue(tR.Find(t.Id).Boredom == 0);
            Assert.IsTrue(tR.Find(t.Id).Pennies == 80);
        }

        [Test]
        public void StartNight_Restroom()
        {
            IHotelTamagotchiContext c = new FakeHotelTamagotchiContext();
            IHotelRoomRepository hR = new HotelRoomRepository(c);
            ITamagotchiRepository tR = new TamagotchiRepository(c);

            HotelRoomViewModel h = new HotelRoomViewModel()
            {
                Size = 5,
                Type = HotelRoomType.Restroom,
            };
            hR.Add(h);

            TamagotchiViewModel t = new TamagotchiViewModel()
            {
                Name = "Test",
                Alive = true,
                HotelRoom = h,
                HotelRoomId = h.Id,
                Boredom = 0,
                Health = 0,
            };
            TamagotchiViewModel t2 = new TamagotchiViewModel()
            {
                Name = "Test2",
                Alive = true,
                HotelRoom = h,
                HotelRoomId = h.Id,
                Boredom = 100,
                Health = 100,
                Pennies = 0
            };
            tR.Add(t);
            tR.Add(t2);

            NightController nC = new NightController(tR, hR);
            nC.StartNight();

            Assert.IsTrue(tR.Find(t.Id).Boredom == 10);
            Assert.IsTrue(tR.Find(t.Id).Pennies == 90);
            Assert.IsTrue(tR.Find(t.Id).Health == 20);

            Assert.IsTrue(tR.Find(t2.Id).Boredom == 100);
            Assert.IsTrue(tR.Find(t2.Id).Pennies == -10);
            Assert.IsTrue(tR.Find(t2.Id).Health == 80);
        }

        [Test]
        public void StartNight_Workroom()
        {
            IHotelTamagotchiContext c = new FakeHotelTamagotchiContext();
            IHotelRoomRepository hR = new HotelRoomRepository(c);
            ITamagotchiRepository tR = new TamagotchiRepository(c);

            HotelRoomViewModel h = new HotelRoomViewModel()
            {
                Size = 5,
                Type = HotelRoomType.Workroom,
            };
            hR.Add(h);

            TamagotchiViewModel t = new TamagotchiViewModel()
            {
                Name = "Test",
                Alive = true,
                HotelRoom = h,
                HotelRoomId = h.Id,
                Boredom = 0,
                Pennies = 0
            };
            TamagotchiViewModel t2 = new TamagotchiViewModel()
            {
                Name = "Test2",
                Alive = true,
                HotelRoom = h,
                HotelRoomId = h.Id,
                Boredom = 100,
            };
            tR.Add(t);
            tR.Add(t2);

            NightController nC = new NightController(tR, hR);
            nC.StartNight();

            Assert.IsTrue(tR.Find(t.Id).Boredom == 20);
            Assert.IsTrue(tR.Find(t.Id).Pennies >= 10 && tR.Find(t.Id).Pennies <= 60);

            Assert.IsTrue(tR.Find(t2.Id).Boredom == 100);
        }

        [Test]
        public void StartNight_Homeless()
        {
            IHotelTamagotchiContext c = new FakeHotelTamagotchiContext();
            IHotelRoomRepository hR = new HotelRoomRepository(c);
            ITamagotchiRepository tR = new TamagotchiRepository(c);


            TamagotchiViewModel t = new TamagotchiViewModel()
            {
                Name = "Test",
                Alive = true
            };
            TamagotchiViewModel t2 = new TamagotchiViewModel()
            {
                Name = "Test2",
                Alive = true,
                Health = 10,
            };
            tR.Add(t);
            tR.Add(t2);

            NightController nC = new NightController(tR, hR);
            nC.StartNight();

            Assert.IsTrue(tR.Find(t.Id).Boredom == 20);
            Assert.IsTrue(tR.Find(t.Id).Health == 80);

            Assert.IsTrue(tR.Find(t2.Id).Health == 0);
            Assert.IsTrue(tR.Find(t2.Id).Alive == false);
        }
    }
}
