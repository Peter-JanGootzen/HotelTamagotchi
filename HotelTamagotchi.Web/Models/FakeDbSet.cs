using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelTamagotchi.Web.Models
{
    public class FakeDbSet<HotelTamagotchiEntity> : IDbSet<HotelTamagotchiEntity> where HotelTamagotchiEntity : BaseHotelTamagotchiEntity
    {
        ObservableCollection<HotelTamagotchiEntity> _data;
        IQueryable _query;

        public FakeDbSet()
        {
            _data = new ObservableCollection<HotelTamagotchiEntity>();
            _query = _data.AsQueryable();
        }

        public HotelTamagotchiEntity Find(params object[] keyValues)
        {
            return this.SingleOrDefault(e => e.Id == (int)keyValues.Single());
        }

        public virtual HotelTamagotchiEntity Add(HotelTamagotchiEntity item)
        {
            int highestId = 0;
            foreach (BaseHotelTamagotchiEntity h in this.ToList())
            {
                if (h.Id > highestId)
                    highestId = h.Id;
            }
            item.Id = highestId;
            _data.Add(item);
            return item;
        }

        public HotelTamagotchiEntity Remove(HotelTamagotchiEntity item)
        {
            _data.Remove(item);
            return item;
        }

        public HotelTamagotchiEntity Attach(HotelTamagotchiEntity item)
        {
            _data.Add(item);
            return item;
        }

        public HotelTamagotchiEntity Detach(HotelTamagotchiEntity item)
        {
            _data.Remove(item);
            return item;
        }

        public HotelTamagotchiEntity Create()
        {
            return Activator.CreateInstance<HotelTamagotchiEntity>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, HotelTamagotchiEntity
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public ObservableCollection<HotelTamagotchiEntity> Local
        {
            get { return _data; }
        }

        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }

        System.Linq.Expressions.Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return _query.Provider; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<HotelTamagotchiEntity> IEnumerable<HotelTamagotchiEntity>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        
    }
}