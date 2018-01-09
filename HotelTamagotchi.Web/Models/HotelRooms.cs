namespace HotelTamagotchi.Web.Models
{
    using System;
    using System.Collections.Generic;

    public class HotelRoom
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HotelRoom()
        {
            this.Tamagotchis = new HashSet<Tamagotchi>();
        }

        public int Id { get; set; }
        public byte Size { get; set; }
        public HotelRoomType Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tamagotchi> Tamagotchis { get; set; }
    }
}