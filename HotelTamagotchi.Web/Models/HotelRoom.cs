namespace HotelTamagotchi.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HotelRoom")]
    public partial class HotelRoom : BaseHotelTamagotchiEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HotelRoom()
        {
            Tamagotchi = new HashSet<Tamagotchi>();
        }

        public byte Size { get; set; }

        public HotelRoomType Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tamagotchi> Tamagotchi { get; set; }
    }
}
