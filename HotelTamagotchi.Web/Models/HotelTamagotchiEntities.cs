using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace HotelTamagotchi.Web.Models
{
    public partial class HotelTamagotchiEntities : DbContext
    {
        public HotelTamagotchiEntities()
            : base("name=HotelTamagotchiEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<HotelRoom> HotelRooms { get; set; }
        public virtual DbSet<Tamagotchi> Tamagotchis { get; set; }
    }
}