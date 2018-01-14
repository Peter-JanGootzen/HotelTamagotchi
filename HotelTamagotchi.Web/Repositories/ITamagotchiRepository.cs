﻿using HotelTamagotchi.Web.Models;
using HotelTamagotchi.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelTamagotchi.Web.Repositories
{
    public interface ITamagotchiRepository : IRepository<TamagotchiViewModel>
    {
        List<TamagotchiViewModel> GetAllHomelessTamagotchi();
        List<TamagotchiViewModel> GetAllFromUser(int userId);
    }
}
