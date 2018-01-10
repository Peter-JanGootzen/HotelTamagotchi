using HotelTamagotchi.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelTamagotchi.Web.ViewModels
{
    public class TamagotchiViewModel : IValidatableObject
    {
        Tamagotchi _model;

        public TamagotchiViewModel()
        {
            _model = new Tamagotchi();
        }

        public TamagotchiViewModel(Tamagotchi tamagotchi)
        {
            _model = tamagotchi;
        }


        #region Properties
        public String Name
        {
            get => _model.Name;
            set => _model.Name = value;
        }

        public int Id
        {
            get => _model.Id;
            set => _model.Id = value;
        }
        public int? HotelRoomId
        {
            get => _model.HotelRoomId;
            set => _model.HotelRoomId = value;
        }
        public int Pennies
        {
            get => _model.Pennies;
            set => _model.Pennies = value;
        }
        public int Age
        {
            get => _model.Age;
            set => _model.Age = value;
        }
        public int Level
        {
            get => _model.Level;
            set => _model.Level = value;
        }
        public byte Health
        {
            get => _model.Health;
            set => _model.Health = value;
        }
        public byte Boredom
        {
            get => _model.Boredom;
            set => _model.Boredom = value;
        }
        public bool Alive
        {
            get => _model.Alive;
            set => _model.Alive = value;
        }

        public HotelRoom HotelRoom
        {
            get => _model.HotelRoom;
            set => _model.HotelRoom = value;
        }
#endregion

        public void SleepOutside()
        {
            if (_model.HotelRoom != null)
            {
                if (_model.Health - 20 >= 0)
                    _model.Health -= 20;
                else
                    _model.Health = 0;
                if (_model.Boredom + 20 <= 100)
                    _model.Boredom += 20;
                else
                    _model.Boredom = 10;
            }
        }

        public Tamagotchi ToModel()
        {
            return _model;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            var pName = new[] { "Name" };
            var pPennies = new[] { "Pennies" };
            var pAge = new[] { "Age" };
            var pLevel = new[] { "Level" };
            var pHealth = new[] { "Health" };
            var pBoredom = new[] { "Boredom" };
            var pAlive = new[] { "Alive" };

            if (!String.IsNullOrWhiteSpace(Name))
            {
                if (Name.Length > 10)
                {
                    errors.Add(new ValidationResult("Je naam mag maximaal 10 letters bevatten!", pName));
                }
            }
            else
            {
                errors.Add(new ValidationResult("Je moet een naam invullen!", pName));
            }
            if (Age < 0)
            {
                errors.Add(new ValidationResult("Je moet minimaal een leeftijd hebben van 0", pAge));
            }
            if (Pennies < 0)
            {
                errors.Add(new ValidationResult("Je moet minimaal 0 of meer centjes hebben!", pPennies));
            }
            if (Level < 0)
            {
                errors.Add(new ValidationResult("Je moet minimaal een level hebben van 0 of meer!", pLevel));
            }
            if (Health < 0 || Health > 100)
            {
                errors.Add(new ValidationResult("Je levens moet mininmaal tussen 0 en de 100 liggen", pHealth));
            }
            if (Boredom < 0 || Boredom > 100)
            {
                errors.Add(new ValidationResult("Je verveling moet mininmaal tussen 0 en de 100 liggen", pBoredom));
            }

            return errors;
        }

        public override string ToString()
        {
            return _model.Name;
        }
    }
}