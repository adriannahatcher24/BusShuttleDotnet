using System;
using System.Collections.Generic;
using System.Linq;
using DomainModel;
using Microsoft.VisualBasic;
using WebMvc.Database;
namespace WebMvc.Service
{
    public class BusService : BusServiceInterface
    {
        private readonly userDb _userDb;

        public BusService(userDb userDb)
        {
            _userDb = userDb;
        }
        public List<BusModel> GetBusses()
        {
            var busList = _userDb.Bus.Select(b => new BusModel(b.Id, b.BusNumber)).ToList();
            return busList;
        }

        public void UpdateBusByID(int id, int busNumber)
        {
            var bus = _userDb.Bus.FirstOrDefault(b => b.Id == id);
            if (bus != null)
            {
                bus.BusNumber = busNumber;
                _userDb.SaveChanges();

            }
        }

        public void CreateBus(int busNumber)
        {
            var newBus = new Database.Bus
            {
                BusNumber = busNumber
            };
            _userDb.Bus.Add(newBus);
            _userDb.SaveChanges();

        }

        public BusModel? FindBusByID(int id)
        {
            var bus = _userDb.Bus.FirstOrDefault(b => b.Id == id);
            if (bus != null)
            {
                return new BusModel(bus.Id, bus.BusNumber);
            }
            return null;
        }
        public void DeleteBus(int id)
        {
            var bus = _userDb.Bus.FirstOrDefault(b => b.Id == id);
            if (bus != null)
            {
                _userDb.Bus.Remove(bus);
                _userDb.SaveChanges();
            }
        }
    }





}