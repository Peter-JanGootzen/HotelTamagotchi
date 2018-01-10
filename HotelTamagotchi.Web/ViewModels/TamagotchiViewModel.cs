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

        // TODO properties

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
    }
}