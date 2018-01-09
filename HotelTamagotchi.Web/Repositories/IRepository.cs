using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelTamagotchi.Web.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Find(object id);
        void SetChanged(T entity);
        IList<T> GetAll();
        void Add(T entity);
        void Remove(T entity);
        void Dispose();
    }
}
