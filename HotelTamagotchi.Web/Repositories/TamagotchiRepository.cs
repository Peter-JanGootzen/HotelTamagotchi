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
        IHotelTamagotchiContext _database;

        public TamagotchiRepository(IHotelTamagotchiContext database)
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
            if (_database.Tamagotchi.Find(id) != null)
            {
                return new TamagotchiViewModel(_database.Tamagotchi.Find(id));
            }
            return null;
        }

        public List<TamagotchiViewModel> GetAll()
        {
            List<TamagotchiViewModel> list = new List<TamagotchiViewModel>();
            foreach (Tamagotchi t in _database.Tamagotchi)
            {
                list.Add(new TamagotchiViewModel(t));
            }
            return list;
        }

        public List<TamagotchiViewModel> GetAllHomelessTamagotchi()
        {
            return GetAll().Where(t => t.HotelRoom == null).ToList();
        }

        public void Remove(TamagotchiViewModel entity)
        {
            _database.Tamagotchi.Remove(entity.ToModel());
            _database.SaveChanges();
        }

        public void SetChanged(TamagotchiViewModel entity)
        {
            _database.SetChanged(entity.ToModel());
            _database.SaveChanges();
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}