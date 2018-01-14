namespace HotelTamagotchi.Web.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Infrastructure;

    public partial class HotelTamagotchiContext : DbContext, IHotelTamagotchiContext
    {
        public HotelTamagotchiContext()
            : base("name=HotelTamagotchiContext")
        {
        }

        public virtual IDbSet<HotelRoom> HotelRoom { get; set; }
        public virtual IDbSet<Tamagotchi> Tamagotchi { get; set; }
        public virtual IDbSet<User> User { get; set; }

        public void SetChanged(BaseHotelTamagotchiEntity baseHotelTamagotchiEntity)
        {
            this.Entry(baseHotelTamagotchiEntity).State = EntityState.Modified;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tamagotchi>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}
