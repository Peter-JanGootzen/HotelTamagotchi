using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace HotelTamagotchi.Web.Models
{
    public partial class HotelTamagotchiEntities : DbContext
    {
        public HotelTamagotchiEntities() : base("name=HotelTamagotchiEntities")
        {
        }

        public virtual DbSet<HotelRoom> HotelRooms { get; set; }
        public virtual DbSet<Tamagotchi> Tamagotchis { get; set; }
    }
}