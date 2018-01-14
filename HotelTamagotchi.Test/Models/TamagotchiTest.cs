using HotelTamagotchi.Web.Controllers;
using HotelTamagotchi.Web.Models;
using HotelTamagotchi.Web.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelTamagotchi.Test.Models
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
            TamagotchiViewModel t = new TamagotchiViewModel() { Age = age, Name = name, Pennies = pennies, Level = level, Health = health, Boredom = boredom, Alive = alive  };

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
            TamagotchiViewModel t = new TamagotchiViewModel() { Age = 0, Name = name, Pennies = pennies, Level = level, Health = health, Boredom = boredom, Alive = alive };
            // Act
            var errors = t.Validate(null);
            //Assert
            Assert.AreEqual(0,errors.Count());

        }

        [TestMethod]
        public void Tamagotchi_invalid_validation()
        {
            // Arrange
            TamagotchiViewModel t1 = new TamagotchiViewModel() { Age = -1, Name = "Testestestest", Pennies = -1, Level = -1, Health = 101, Boredom = 101, Alive = alive };
            TamagotchiViewModel t2 = new TamagotchiViewModel() { Age = 0, Name = null, Pennies = pennies, Level = level, Health = health, Boredom = boredom, Alive = alive };

            // Act
            string NameLength = "Your name can only be 10 letters long";
            string NameNull = "You have to put in a name in the name field";
            string AgeCheck = "You have to be atleast 0 days old";
            string PenniesCheck = "You can not have a negative amount of pennies";
            string LevelCheck = "You can not have a negative amount of levels";
            string HealthCheck = "Your health value must be between 0 and 100";
            string BoredomCheck = "Your boredom value must be between 0 and 100";

            var errors1 = t1.Validate(null);
            var errors2 = t2.Validate(null);

            //Assert

            Assert.AreEqual(NameLength, errors1.Where(x => x.ErrorMessage.Equals(NameLength)).FirstOrDefault().ErrorMessage);
            Assert.AreEqual(AgeCheck, errors1.Where(x => x.ErrorMessage.Equals(AgeCheck)).FirstOrDefault().ErrorMessage);
            Assert.AreEqual(PenniesCheck, errors1.Where(x => x.ErrorMessage.Equals(PenniesCheck)).FirstOrDefault().ErrorMessage);
            Assert.AreEqual(LevelCheck, errors1.Where(x => x.ErrorMessage.Equals(LevelCheck)).FirstOrDefault().ErrorMessage);
            Assert.AreEqual(HealthCheck, errors1.Where(x => x.ErrorMessage.Equals(HealthCheck)).FirstOrDefault().ErrorMessage);
            Assert.AreEqual(BoredomCheck, errors1.Where(x => x.ErrorMessage.Equals(BoredomCheck)).FirstOrDefault().ErrorMessage);

            Assert.AreEqual(NameNull, errors2.Where(x => x.ErrorMessage.Equals(NameNull)).FirstOrDefault().ErrorMessage);

        }

    }
}
