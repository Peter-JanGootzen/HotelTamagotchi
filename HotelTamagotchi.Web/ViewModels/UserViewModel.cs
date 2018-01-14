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
        User _model;

        public UserViewModel()
        {
            _model = new User();
        }

        public UserViewModel(User user)
        {
            _model = user;
        }

        public int Id
        {
            get => _model.Id;
            set => _model.Id = value;
        }

        [Required]
        public string Username
        {
            get => _model.Username;
            set => _model.Username = value;
        }

        [Required]
        [DataType(DataType.Password)]
        public string Password
        {
            get => _model.Password;
            set => _model.Password = value;
        }

        public UserRole Role
        {
            get => _model.Role;
            set => _model.Role = value;
        }

        public User ToModel()
        {
            return _model;
        }
    }
}