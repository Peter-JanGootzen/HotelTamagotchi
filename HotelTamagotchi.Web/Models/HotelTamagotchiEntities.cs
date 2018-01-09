namespace HotelTamagotchi.Web.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HotelTamagotchiEntities : DbContext
    {
        public HotelTamagotchiEntities()
            : base("name=HotelTamagotchiEntities")
        {
        }

        public virtual DbSet<HotelRoom> HotelRoom { get; set; }
        public virtual DbSet<Tamagotchi> Tamagotchi { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tamagotchi>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}
