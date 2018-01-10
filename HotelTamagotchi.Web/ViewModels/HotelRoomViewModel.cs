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

        public HotelRoomSize Size
        {
            get => _model.Size;
            set => _model.Size = value;
        }

        public HotelRoomType Type
        {
            get => _model.Type;
            set => _model.Type = value;
        }

        public bool IsBooked
        {
            get => _model.Tamagotchi.Count != 0;
            private set { }
        }

        public List<TamagotchiViewModel> Tamagotchi
        {
            get
            {
                List<TamagotchiViewModel> list = new List<TamagotchiViewModel>();
                foreach(Tamagotchi t in _model.Tamagotchi)
                {
                    list.Add(new TamagotchiViewModel(t));
                }
                return list;
            }
        }

      
        #endregion

        public void AddTamagotchi(Tamagotchi tamagotchi)
        {
            _model.Tamagotchi.Add(tamagotchi);
        }

        public void RemoveTamagotchi(Tamagotchi tamagotchi)
        {
            _model.Tamagotchi.Remove(tamagotchi);
        }
        public HotelRoom ToModel()
        {
            return _model;
        }

        public void ProcessNight()
        {
            if (_model.Type != HotelRoomType.Fightroom)
            {
                foreach (Tamagotchi t in _model.Tamagotchi)
                {
                    switch (_model.Type)
                    {
                        case HotelRoomType.Restroom:
                            t.Pennies -= 10;
                            if (t.Health + 20 <= 100)
                                t.Health += 20;
                            else
                                t.Health = 100;
                            if (t.Boredom + 10 <= 100)
                                t.Boredom += 10;
                            else
                                t.Boredom = 100;
                            break;
                        case HotelRoomType.Gameroom:
                            t.Pennies -= 20;
                            t.Boredom = 0;
                            break;
                        case HotelRoomType.Workroom:
                            Random r = new Random(DateTime.Now.GetHashCode());
                            int penniesRandom = r.Next(10, 60);
                            t.Pennies += penniesRandom;
                            if (t.Boredom + 20 <= 100)
                                t.Boredom += 20;
                            else
                                t.Boredom = 100;
                            break;
                    }
                }
            }
            else
            {
                Random r = new Random(DateTime.Now.GetHashCode());
                int winnerIndex = r.Next(0, _model.Tamagotchi.Count);
                Tamagotchi[] tArray = _model.Tamagotchi.ToArray();
                for (int i = 0; i < _model.Tamagotchi.Count; i++)
                {
                    if (i == winnerIndex)
                    {
                        tArray[i].Pennies += 20;
                        tArray[i].Level += 1;
                    }
                    else
                    {
                        tArray[i].Pennies -= 20;
                        if (tArray[i].Health - 30 >= 0)
                            tArray[i].Health -= 30;
                        else
                            tArray[i].Health = 0;
                    }
                }
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            var size = new[] { "Size" };
            var type = new[] { "Type" };

            if (Size != HotelRoomSize.Bigroom & Size != HotelRoomSize.Mediumroom & Size != HotelRoomSize.Smallroom)
            {
                errors.Add(new ValidationResult("De grootte van een kamer moet een grote, medium of kleine kamer zijn!", size));
            }

            if (Type != HotelRoomType.Workroom & Type != HotelRoomType.Fightroom & Type != HotelRoomType.Restroom & Type != HotelRoomType.Gameroom)
            {
                errors.Add(new ValidationResult("Het type van een kamer moet 'Fightroom', 'Workroom', 'Restroom' of 'Gameroom' zijn!", type));
            }
            return errors;
        }
    }
}