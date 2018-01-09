using HotelTamagotchi.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelTamagotchi.Web.Repositories
{
    public class HotelRoomRepository : IRepository<HotelRoom>
    {
        HotelTamagotchiEntities _database;

        public HotelRoomRepository()
        {
            _database = new HotelTamagotchiEntities();
        }

        public void Add(HotelRoom entity)
        {
            _database.HotelRoom.Add(entity);
            _database.SaveChanges();
        }

        public HotelRoom Find(object id)
        {
            return _database.HotelRoom.Find(id);
        }

        public IList<HotelRoom> GetAll()
        {
            return _database.HotelRoom.Include("Tamagotchi").ToList();
        }

        public void Remove(HotelRoom entity)
        {
            _database.HotelRoom.Remove(entity);
            _database.SaveChanges();
        }

        public void SetChanged(HotelRoom entity)
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