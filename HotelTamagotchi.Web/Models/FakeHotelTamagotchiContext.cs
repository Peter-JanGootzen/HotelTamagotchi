using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace HotelTamagotchi.Web.Models
{
    public class FakeHotelTamagotchiContext : IHotelTamagotchiContext
    {
        bool disposed;
        IDbSet<HotelRoom> _hotelRoom;
        IDbSet<Tamagotchi> _tamagotchi;

        public FakeHotelTamagotchiContext()
        {
            _hotelRoom = new FakeDbSet<HotelRoom>();
            _tamagotchi = new FakeDbSet<Tamagotchi>();
        }
        public IDbSet<HotelRoom> HotelRoom
        {
            get
            {
                if (disposed == true)
                    throw new InvalidOperationException("The operation cannot be completed because the DbContext has been disposed.");
                else
                    return _hotelRoom;
            }
            set
            {
                if (disposed == true)
                    throw new InvalidOperationException("The operation cannot be completed because the DbContext has been disposed.");
                else
                    _hotelRoom = value;
            }
        }
        public IDbSet<Tamagotchi> Tamagotchi
        {
            get
            {
                if (disposed == true)
                    throw new InvalidOperationException("The operation cannot be completed because the DbContext has been disposed.");
                else
                    return _tamagotchi;
            }
            set
            {
                if (disposed == true)
                    throw new InvalidOperationException("The operation cannot be completed because the DbContext has been disposed.");
                else
                    _tamagotchi = value;
            }
        }

        // Disposing is not needed in a FakeContext
        public void Dispose()
        {
            disposed = true;
        }

        public void SetChanged(BaseHotelTamagotchiEntity baseHotelTamagotchiEntity)
        {
            return;
        }

        public int SaveChanges()
        {
            return 0;
        }
    }
}