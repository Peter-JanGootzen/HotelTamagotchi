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

        public HotelRoomViewModel Find(object id)
        {
            return new HotelRoomViewModel(_database.HotelRoom.Find(id));
        }

        public IList<HotelRoomViewModel> GetAll()
        {
            List<HotelRoomViewModel> list = new List<HotelRoomViewModel>();
            foreach(HotelRoom h in _database.HotelRoom)
            {
                list.Add(new HotelRoomViewModel(h));
            }
            return list;
        }

        public void Remove(HotelRoomViewModel entity)
        {
            _database.HotelRoom.Remove(entity.ToModel());
            _database.SaveChanges();
        }

        public void SetChanged(HotelRoomViewModel entity)
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