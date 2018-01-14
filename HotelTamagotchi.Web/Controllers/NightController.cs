using HotelTamagotchi.Web.Models;
using HotelTamagotchi.Web.Repositories;
using HotelTamagotchi.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HotelTamagotchi.Web.Controllers
{
    public class NightController
    {
        private ITamagotchiRepository _tamagotchiRepository;
        private IHotelRoomRepository _hotelRoomRepository;

        public NightController(ITamagotchiRepository tamagotchiRepository, IHotelRoomRepository hotelRoomRepository)
        {
            _tamagotchiRepository = tamagotchiRepository;
            _hotelRoomRepository = hotelRoomRepository;
        }

        public void StartNight()
        {
            foreach (TamagotchiViewModel t in _tamagotchiRepository.GetAll())
            {
                if (t.HotelRoom != null)
                {
                    switch (t.HotelRoom.Type)
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
                            t.LeaveRoom();
                            _tamagotchiRepository.SetChanged(t);
                            break;
                        case HotelRoomType.Gameroom:
                            t.Pennies -= 20;
                            t.Boredom = 0;
                            t.LeaveRoom();
                            _tamagotchiRepository.SetChanged(t);
                            break;
                        case HotelRoomType.Workroom:
                            Random r = new Random(DateTime.Now.GetHashCode());
                            int penniesRandom = r.Next(10, 60);
                            t.Pennies += penniesRandom;
                            if (t.Boredom + 20 <= 100)
                                t.Boredom += 20;
                            else
                                t.Boredom = 100;
                            t.LeaveRoom();
                            _tamagotchiRepository.SetChanged(t);
                            break;
                        case HotelRoomType.Fightroom:
                            break; // Gets handled at a later stage
                    }
                }
                else // Homeless Tamagotchi
                {
                    if (t.Health - 20 >= 0)
                        t.Health -= 20;
                    else
                        t.Health = 0;
                    if (t.Boredom + 20 <= 100)
                        t.Boredom += 20;
                    else
                        t.Boredom = 10;
                    _tamagotchiRepository.SetChanged(t);
                }
            }
            foreach (HotelRoomViewModel h in _hotelRoomRepository.GetAll())
            {
                if (h.Type == HotelRoomType.Fightroom)
                {
                    Random r = new Random(DateTime.Now.GetHashCode());
                    int winnerIndex = r.Next(0, h.Tamagotchi.Count);
                    for (int i = 0; i < h.Tamagotchi.Count; i++)
                    {
                        if (i == winnerIndex)
                        {
                            h.Tamagotchi[i].Pennies += 20;
                            h.Tamagotchi[i].Level += 1;
                        }
                        else
                        {
                            h.Tamagotchi[i].Pennies -= 20;
                            if (h.Tamagotchi[i].Health - 30 >= 0)
                                h.Tamagotchi[i].Health -= 30;
                            else
                            {
                                h.Tamagotchi[i].Health = 0;
                                h.Tamagotchi[i].Alive = false;
                            }
                        }
                        h.Tamagotchi[i].LeaveRoom();
                    }
                }
                else if (h.Type == HotelRoomType.Quidditch)
                {
                    Random r = new Random(DateTime.Now.GetHashCode());
                    bool snitchAlreadyCatched = false;
                    foreach (TamagotchiViewModel t in h.Tamagotchi)
                    {
                        bool scored = r.Next(0, 100) > 60;
                        bool damaged = r.Next(0, 100) > 30;
                        bool catchedSnitch = false;
                        if (snitchAlreadyCatched == false)
                            catchedSnitch = r.Next(0, 100) > 5;

                        if (scored)
                        {
                            t.Pennies += 10;
                            t.Level++;
                        }
                        if (damaged)
                        {
                            t.Health -= 30;
                        }
                        if (catchedSnitch)
                        {
                            t.Pennies += 150;
                            t.Level++;
                        }
                        t.LeaveRoom();
                    }
                }
                _hotelRoomRepository.SetChanged(h);
            }
        }
    }
}