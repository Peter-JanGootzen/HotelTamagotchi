namespace HotelTamagotchi.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HotelRoom")]
    public partial class HotelRoom : IValidatableObject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HotelRoom()
        {
            Tamagotchi = new HashSet<Tamagotchi>();
        }

        public int Id { get; set; }

        public HotelRoomSize Size { get; set; }

        public HotelRoomType Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tamagotchi> Tamagotchi { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            var size = new[] { "Size" };
            var type = new[] { "Type" };

            if(Size != HotelRoomSize.Bigroom & Size != HotelRoomSize.Mediumroom & Size != HotelRoomSize.Smallroom)
            {
                errors.Add(new ValidationResult("De grootte van een kamer moet een grote, medium of kleine kamer zijn!", size));
            }

            if (Type != HotelRoomType.Workroom & Type != HotelRoomType.Fightroom & Type != HotelRoomType.Restroom & Type != HotelRoomType.Gameroom)
            {
                errors.Add(new ValidationResult("Het type van een kamer moet 'Fightroom', 'Workroom', 'Restroom' of 'Gameroom' zijn!", type));
            }
            return errors;
        }
    }
}
