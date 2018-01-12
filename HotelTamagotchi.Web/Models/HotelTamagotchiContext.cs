namespace HotelTamagotchi.Web.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HotelTamagotchiContext : DbContext, IHotelTamagotchiContext
    {
        public HotelTamagotchiContext()
            : base("name=HotelTamagotchiContext")
        {
        }

        public virtual IDbSet<HotelRoom> HotelRoom { get; set; }
        public virtual IDbSet<Tamagotchi> Tamagotchi { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tamagotchi>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}
