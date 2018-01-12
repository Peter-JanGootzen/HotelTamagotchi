using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelTamagotchi.Web.Models
{
    public abstract class FakeDbSet<IHotelTamagotchiEntity> : IDbSet<IHotelTamagotchiEntity>
    where IHotelTamagotchiEntity : class
    {
        ObservableCollection<IHotelTamagotchiEntity> _data;
        IQueryable _query;

        public FakeDbSet()
        {
            _data = new ObservableCollection<IHotelTamagotchiEntity>();
            _query = _data.AsQueryable();
        }

        public abstract IHotelTamagotchiEntity Find(params object[] keyValues);

        public IHotelTamagotchiEntity Add(IHotelTamagotchiEntity item)
        {
            _data.Add(item);
            return item;
        }

        public IHotelTamagotchiEntity Remove(IHotelTamagotchiEntity item)
        {
            _data.Remove(item);
            return item;
        }

        public IHotelTamagotchiEntity Attach(IHotelTamagotchiEntity item)
        {
            _data.Add(item);
            return item;
        }

        public IHotelTamagotchiEntity Detach(IHotelTamagotchiEntity item)
        {
            _data.Remove(item);
            return item;
        }

        public IHotelTamagotchiEntity Create()
        {
            return Activator.CreateInstance<IHotelTamagotchiEntity>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, IHotelTamagotchiEntity
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public ObservableCollection<IHotelTamagotchiEntity> Local
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

        IEnumerator<IHotelTamagotchiEntity> IEnumerable<IHotelTamagotchiEntity>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        
    }
}