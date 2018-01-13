using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelTamagotchi.Web.Models
{
    public interface IHotelTamagotchiContext : IDisposable
    {
        IDbSet<HotelRoom> HotelRoom { get; }
        IDbSet<Tamagotchi> Tamagotchi { get; }
        void SetChanged(BaseHotelTamagotchiEntity baseHotelTamagotchiEntity);
        int SaveChanges();
    }
}
