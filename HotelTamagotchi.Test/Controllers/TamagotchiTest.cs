using HotelTamagotchi.Web.Controllers;
using HotelTamagotchi.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelTamagotchi.Test.Controllers
{
    [TestClass]
    public class TamagotchiTest
    {
        int age = 0;
        string name = "testname";
        int pennies = 0;
        int level = 20;
        byte health = 90;
        byte boredom = 100;
        bool alive = false;

        [TestMethod]
        public void Tamagotchi_properties()
        {
            // Arrange
            Tamagotchi t = new Tamagotchi() { Age = age, Name = name, Pennies = pennies, Level = level, Health = health, Boredom = boredom, Alive = alive  };

            // Act
            

            // Assert
            Assert.AreEqual(age, t.Age);
            Assert.AreEqual(name, t.Name);
            Assert.AreEqual(pennies, t.Pennies);
            Assert.AreEqual(level, t.Level);
            Assert.AreEqual(health, t.Health);
            Assert.AreEqual(boredom, t.Boredom);
            Assert.AreEqual(alive, t.Alive);

        }
        [TestMethod]
        public void Tamagotchi_valid_validation()
        {
            // Arrange
            Tamagotchi t = new Tamagotchi() { Age = 0, Name = name, Pennies = pennies, Level = level, Health = health, Boredom = boredom, Alive = alive };
            // Act
            var errors = t.Validate(null);
            //Assert
            Assert.AreEqual(new List<ValidationResult>().Count,errors.Count());

        }

        [TestMethod]
        public void Tamagotchi_invalid_validation()
        {
            // Arrange
            Tamagotchi t1 = new Tamagotchi() { Age = -1, Name = "Testestestest", Pennies = -1, Level = -1, Health = 101, Boredom = 101, Alive = alive };
            Tamagotchi t2 = new Tamagotchi() { Age = 0, Name = null, Pennies = pennies, Level = level, Health = health, Boredom = boredom, Alive = alive };

            // Act
            string NameNull = "Je moet een naam invullen!";
            string NameLength = "Je naam mag maximaal 10 letters bevatten!";
            string AgeCheck = "Je moet minimaal een leeftijd hebben van 0";
            string PenniesCheck = "Je moet minimaal 0 of meer centjes hebben!";
            string LevelCheck = "Je moet minimaal een level hebben van 0 of meer!";
            string HealthCheck = "Je levens moet mininmaal tussen 0 en de 100 liggen";
            string BoredomCheck = "Je verveling moet mininmaal tussen 0 en de 100 liggen";

            var errors1 = t1.Validate(null);
            var errors2 = t2.Validate(null);

            //Assert

            Assert.AreEqual(NameLength, errors1.Where(x => x.ErrorMessage.Equals("Je naam mag maximaal 10 letters bevatten!")).FirstOrDefault().ErrorMessage);
            Assert.AreEqual(AgeCheck, errors1.Where(x => x.ErrorMessage.Equals("Je moet minimaal een leeftijd hebben van 0")).FirstOrDefault().ErrorMessage);
            Assert.AreEqual(PenniesCheck, errors1.Where(x => x.ErrorMessage.Equals("Je moet minimaal 0 of meer centjes hebben!")).FirstOrDefault().ErrorMessage);
            Assert.AreEqual(LevelCheck, errors1.Where(x => x.ErrorMessage.Equals("Je moet minimaal een level hebben van 0 of meer!")).FirstOrDefault().ErrorMessage);
            Assert.AreEqual(HealthCheck, errors1.Where(x => x.ErrorMessage.Equals("Je levens moet mininmaal tussen 0 en de 100 liggen")).FirstOrDefault().ErrorMessage);
            Assert.AreEqual(BoredomCheck, errors1.Where(x => x.ErrorMessage.Equals("Je verveling moet mininmaal tussen 0 en de 100 liggen")).FirstOrDefault().ErrorMessage);

            Assert.AreEqual(NameNull, errors2.Where(x => x.ErrorMessage.Equals("Je moet een naam invullen!")).FirstOrDefault().ErrorMessage);

        }

    }
}
