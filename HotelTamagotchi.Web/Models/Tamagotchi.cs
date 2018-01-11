namespace HotelTamagotchi.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tamagotchi")]
    public partial class Tamagotchi : IValidatableObject
    {
        public int Id { get; set; }

        public int? HotelRoomId { get; set; }

        public string Name { get; set; }

        public int Pennies { get; set; }

        public int Age { get; set; }

        public int Level { get; set; }

        public byte Health { get; set; }

        public byte Boredom { get; set; }

        public bool Alive { get; set; }

        public virtual HotelRoom HotelRoom { get; set; }

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
