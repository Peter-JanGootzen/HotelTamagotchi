using HotelTamagotchi.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelTamagotchi.Web.Repositories
{
    public interface IUserRepository : IRepository<UserViewModel>
    {
        bool Exists(string username);
        UserViewModel Authenticate(string username, string password);
    }
}
