using HotelTamagotchi.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelTamagotchi.Web.Repositories
{
    public class UserRepository : IRepository<User>
    {
        IHotelTamagotchiContext _context;

        public UserRepository(IHotelTamagotchiContext context)
        {
            _context = context;
        }

        public void Add(User entity)
        {
            _context.User.Add(entity);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public User Find(object id)
        {
            return _context.User.Find(id);
        }

        public List<User> GetAll()
        {
            return _context.User.ToList();
        }

        public void Remove(User entity)
        {
            _context.User.Remove(entity);
            _context.SaveChanges();
        }

        public void SetChanged(User entity)
        {
            _context.SetChanged(entity);
            _context.SaveChanges();
        }
    }
}