using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelTamagotchi.Web.Models
{
    public class FakeTamagotchiSet : FakeDbSet<Tamagotchi>
    {
        public override Tamagotchi Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.Id == (int)keyValues.Single());
        }
    }
}