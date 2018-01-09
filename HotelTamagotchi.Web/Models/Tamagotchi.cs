namespace HotelTamagotchi.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tamagotchi")]
    public partial class Tamagotchi
    {
        public int Id { get; set; }

        public int? HotelRoomId { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        public int Pennies { get; set; }

        public int Age { get; set; }

        public int Level { get; set; }

        public byte Health { get; set; }

        public byte Boredom { get; set; }

        public bool Alive { get; set; }

        public virtual HotelRoom HotelRoom { get; set; }
    }
}
