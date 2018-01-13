using HotelTamagotchi.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelTamagotchi.Web.ViewModels
{
    public class HotelRoomViewModel : IValidatableObject
    {
        HotelRoom _model;


        public HotelRoomViewModel(HotelRoom model)
        {
            _model = model;
        }

        public HotelRoomViewModel()
        {
            _model = new HotelRoom();
        }

        #region Properties
        public int Id
        {
            get => _model.Id;
            set => _model.Id = value;
        }

        public byte Size
        {
            get => _model.Size;
            set => _model.Size = value;
        }

        public HotelRoomType Type
        {
            get => _model.Type;
            set => _model.Type = value;
        }

        public bool IsBooked() => _model.Tamagotchi.Count != 0;

        public List<TamagotchiViewModel> Tamagotchi
        {
            get => _model.Tamagotchi.Select(t => new TamagotchiViewModel(t)).ToList();
        }

      
        #endregion

        public void AddTamagotchi(TamagotchiViewModel tamagotchi)
        {
            _model.Tamagotchi.Add(tamagotchi.ToModel());
        }

        public void RemoveTamagotchi(TamagotchiViewModel tamagotchi)
        {
            _model.Tamagotchi.Remove(tamagotchi.ToModel());
        }
        public HotelRoom ToModel()
        {
            return _model;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            var size = new[] { "Size" };
            var type = new[] { "Type" };

            if (Size != 2 & Size != 3 & Size != 5)
            {
                errors.Add(new ValidationResult("The room has to be 2,3 or 5 spaces big!", size));
            }

            if (Type != HotelRoomType.Workroom & Type != HotelRoomType.Fightroom & Type != HotelRoomType.Restroom & Type != HotelRoomType.Gameroom)
            {
                errors.Add(new ValidationResult("The type of the room has to be 'Fightroom', 'Workroom', 'Restroom' or 'Gameroom'!", type));
            }
            return errors;
        }
    }
}