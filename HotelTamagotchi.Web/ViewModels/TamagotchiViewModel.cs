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
            _model = new Tamagotchi()
            {
                Age = 0,
                Boredom = 0,
                Health = 100,
                Level = 0,
                Pennies = 100
            };
        }

        public TamagotchiViewModel(Tamagotchi tamagotchi)
        {
            _model = tamagotchi;
        }

        #region Properties
        [Required]
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
        public int UserId
        {
            get => _model.UserId;
            set => _model.UserId = value;
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

        public HotelRoomViewModel HotelRoom
        {
            get
            {
                if (_model.HotelRoom == null)
                    return null;
                else
                    return new HotelRoomViewModel(_model.HotelRoom);
            }
            set
            {
                _model.HotelRoom = value.ToModel();
            }
        }

        public UserViewModel User
        {
            get
            {
                if (_model.User == null)
                    return null;
                else
                    return new UserViewModel(_model.User);
            }
            set
            {
                _model.User = value.ToModel();
            }
        }
        #endregion

        public void LeaveRoom()
        {
            HotelRoom = null;
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
                    errors.Add(new ValidationResult("Your name can only be 10 letters long", pName));
                }
            }
            else
            {
                errors.Add(new ValidationResult("You have to put in a name in the name field", pName));
            }
            if (Age < 0)
            {
                errors.Add(new ValidationResult("You have to be atleast 0 days old", pAge));
            }
            if (Pennies < 0)
            {
                errors.Add(new ValidationResult("You can not have a negative amount of pennies", pPennies));
            }
            if (Level < 0)
            {
                errors.Add(new ValidationResult("You can not have a negative amount of levels", pLevel));
            }
            if (Health < 0 || Health > 100)
            {
                errors.Add(new ValidationResult("Your health value must be between 0 and 100", pHealth));
            }
            if (Boredom < 0 || Boredom > 100)
            {
                errors.Add(new ValidationResult("Your boredom value must be between 0 and 100", pBoredom));
            }

            return errors;
        }
    }
}