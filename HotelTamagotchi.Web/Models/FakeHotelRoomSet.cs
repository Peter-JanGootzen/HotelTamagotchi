using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelTamagotchi.Web.Models
{
    public class FakeHotelRoomSet : FakeDbSet<HotelRoom>
    {
        public override HotelRoom Find(params object[] keyValues)
        {
            return this.SingleOrDefault(h => h.Id == (int)keyValues.Single());
        }
    }
}