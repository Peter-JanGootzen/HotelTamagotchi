using HotelTamagotchi.Web.Models;
using HotelTamagotchi.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelTamagotchi.Web.Repositories
{
    public class HotelRoomRepository : IHotelRoomRepository
    {
        HotelTamagotchiEntities _database;

        public HotelRoomRepository(HotelTamagotchiEntities database)
        {
            _database = database;
        }

        public void Add(HotelRoomViewModel entity)
        {
            _database.HotelRoom.Add(entity.ToModel());
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