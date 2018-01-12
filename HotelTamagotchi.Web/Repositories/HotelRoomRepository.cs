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
        IHotelTamagotchiContext _database;

        public HotelRoomRepository(IHotelTamagotchiContext database)
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
            if (_database.HotelRoom.Find(id) != null)
            {
                return new HotelRoomViewModel(_database.HotelRoom.Find(id));
            }
            return null;
        }

        public List<HotelRoomViewModel> GetAll()
        {
            List<HotelRoomViewModel> list = new List<HotelRoomViewModel>();
            foreach (HotelRoom h in _database.HotelRoom)
            {
                list.Add(new HotelRoomViewModel(h));
            }
            return list;
        }

        public List<HotelRoomViewModel> GetAllAvailableHotelRooms()
        {
            return GetAll().Where(h => !h.IsBooked()).ToList();
        }

        public void Remove(HotelRoomViewModel entity)
        {
            _database.HotelRoom.Remove(entity.ToModel());
            _database.SaveChanges();
        }

        public void SetChanged(HotelRoomViewModel entity)
        {
            _database.SaveChanges();
        }
        public void Dispose()
        {
            _database.Dispose();
        }
    }
}