using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelTamagotchi.Web.Models
{
    public abstract class FakeDbSet<TEnity> : IDbSet<TEnity>
    where TEnity : class
    {
        ObservableCollection<TEnity> _data;
        IQueryable _query;

        public FakeDbSet()
        {
            _data = new ObservableCollection<TEnity>();
            _query = _data.AsQueryable();
        }

        public abstract TEnity Find(params object[] keyValues);

        public TEnity Add(TEnity item)
        {
            _data.Add(item);
            return item;
        }

        public TEnity Remove(TEnity item)
        {
            _data.Remove(item);
            return item;
        }

        public TEnity Attach(TEnity item)
        {
            _data.Add(item);
            return item;
        }

        public TEnity Detach(TEnity item)
        {
            _data.Remove(item);
            return item;
        }

        public TEnity Create()
        {
            return Activator.CreateInstance<TEnity>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEnity
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public ObservableCollection<TEnity> Local
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

        IEnumerator<TEnity> IEnumerable<TEnity>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        
    }
}