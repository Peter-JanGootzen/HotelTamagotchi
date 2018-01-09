namespace HotelTamagotchi.Web.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Tamagotchi
    {
        public int Id { get; set; }
        public Nullable<int> HotelRoomId { get; set; }
        public string Name { get; set; }
        public int Pennies { get; set; }
        public int Level { get; set; }
        public byte Health { get; set; }
        public byte Boredom { get; set; }
        public byte Alive { get; set; }

        public virtual HotelRoom HotelRoom { get; set; }
    }
}
