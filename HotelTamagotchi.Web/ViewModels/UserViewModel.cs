using HotelTamagotchi.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelTamagotchi.Web.ViewModels
{
    public class UserViewModel
    {
        User _user;

        public UserViewModel(User user)
        {
            _user = user;
        }

        [Required]
        public string Username
        {
            get => _user.Username;
            set => _user.Username = value;
        }

        [Required]
        [DataType(DataType.Password)]
        public string Password
        {
            get => _user.Password;
            set => _user.Password = value;
        }

        public UserRole Role
        {
            get => _user.Role;
            private set { }
        }
    }
}