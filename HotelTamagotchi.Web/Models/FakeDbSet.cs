using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelTamagotchi.Web.Models
{
    public abstract class FakeDbSet<TEntity> : IDbSet<TEntity>
    where TEntity : class
    {
        ObservableCollection<TEntity> _data;
        IQueryable _query;

        public FakeDbSet()
        {
            _data = new ObservableCollection<TEntity>();
            _query = _data.AsQueryable();
        }

        public abstract TEntity Find(params object[] keyValues);

        public TEntity Add(TEntity item)
        {
            _data.Add(item);
            return item;
        }

        public TEntity Remove(TEntity item)
        {
            _data.Remove(item);
            return item;
        }

        public TEntity Attach(TEntity item)
        {
            _data.Add(item);
            return item;
        }

        public TEntity Detach(TEntity item)
        {
            _data.Remove(item);
            return item;
        }

        public TEntity Create()
        {
            return Activator.CreateInstance<TEntity>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public ObservableCollection<TEntity> Local
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

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        
    }
}