using HotelTamagotchi.Web.Models;
using HotelTamagotchi.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelTamagotchi.Web.Repositories
{
    public class UserRepository : IRepository<UserViewModel>, IUserRepository
    {
        IHotelTamagotchiContext _context;

        public UserRepository(IHotelTamagotchiContext context)
        {
            _context = context;
        }

        public void Add(UserViewModel entity)
        {
            _context.User.Add(entity.ToModel());
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public UserViewModel Find(object id)
        {
            return new UserViewModel(_context.User.Find(id));
        }

        public bool Exists(string username)
        {
            User user = _context.User.Where(x => x.Username.Equals(username)).FirstOrDefault();
            return user == null;
        }

        public List<UserViewModel> GetAll()
        {
            List<UserViewModel> list = new List<UserViewModel>();
            foreach (User u in _context.User)
            {
                list.Add(new UserViewModel(u));
            }
            return list;
        }

        public UserViewModel Authenticate(string username, string password)
        {
            return new UserViewModel(_context.User.Where(x => x.Username.Equals(username) && x.Password.Equals(password)).FirstOrDefault());
        }

        public void Remove(UserViewModel entity)
        {
            _context.User.Remove(entity.ToModel());
            _context.SaveChanges();
        }

        public void SetChanged(UserViewModel entity)
        {
            _context.SetChanged(entity.ToModel());
            _context.SaveChanges();
        }
    }
}