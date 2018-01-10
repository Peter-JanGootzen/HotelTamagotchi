using HotelTamagotchi.Web.Models;
using HotelTamagotchi.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelTamagotchi.Web.Repositories
{
    public class TamagotchiRepository : ITamagotchiRepository
    {
        HotelTamagotchiEntities _database;

        public TamagotchiRepository(HotelTamagotchiEntities database)
        {
            _database = database;
        }


        public void Add(TamagotchiViewModel entity)
        {
            _database.Tamagotchi.Add(entity.ToModel());
            _database.SaveChanges();
        }

        public TamagotchiViewModel Find(object id)
        {
            return new TamagotchiViewModel(_database.Tamagotchi.Find(id));
        }

        public IList<TamagotchiViewModel> GetAll()
        {
            List<TamagotchiViewModel> list = new List<TamagotchiViewModel>();
            foreach (Tamagotchi t in _database.Tamagotchi)
            {
                list.Add(new TamagotchiViewModel(t));
            }
            return list;
        }

        public void Remove(TamagotchiViewModel entity)
        {
            _database.Tamagotchi.Remove(entity.ToModel());
            _database.SaveChanges();
        }

        public void SetChanged(TamagotchiViewModel entity)
        {
            _database.Entry(entity.ToModel()).State = System.Data.Entity.EntityState.Modified;
            _database.SaveChanges();
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}