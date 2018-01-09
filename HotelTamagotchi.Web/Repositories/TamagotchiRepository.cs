using HotelTamagotchi.Web.Models;
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


        public void Add(Tamagotchi entity)
        {
            _database.Tamagotchi.Add(entity);
            _database.SaveChanges();
        }

        public Tamagotchi Find(object id)
        {
            return _database.Tamagotchi.Find(id);
        }

        public IList<Tamagotchi> GetAll()
        {
            return _database.Tamagotchi.Include("HotelRoom").ToList();
        }

        public void Remove(Tamagotchi entity)
        {
            _database.Tamagotchi.Remove(entity);
            _database.SaveChanges();
        }

        public void SetChanged(Tamagotchi entity)
        {
            _database.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _database.SaveChanges();
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}