using System;
using System.Collections.Generic;
using System.Linq;
using DomainModel;
using Microsoft.VisualBasic;
using WebMvc.Database;
namespace WebMvc.Service
{
    public class StopService : StopServiceInterface
    {
        private readonly userDb _userDb;

        public StopService(userDb userDb)
        {
            _userDb = userDb;
        }
        public List<StopModel> GetStops()
        {
            var stopList = _userDb.Stop.Select(s => new StopModel(s.Id, s.Name, s.Latitude, s.Longitude)).ToList();
            return stopList;
        }

        public void UpdateStopByID(int id, string name, double latitude, double longitude)
        {
            var stop = _userDb.Stop.FirstOrDefault(s => s.Id == id);
            if (stop != null)
            {
                stop.Name = name;
                stop.Latitude = latitude;
                stop.Longitude = longitude;
                _userDb.SaveChanges();

            }
        }

        public void CreateStop(string name, double latitude, double longitude)
        {
            var newStop = new Database.Stop
            {
                Name = name,
                Latitude = latitude,
                Longitude = longitude
            };
            _userDb.Stop.Add(newStop);
            _userDb.SaveChanges();

        }

        public StopModel? FindStopByID(int id)
        {
            var stop = _userDb.Stop.FirstOrDefault(s => s.Id == id);
            if (stop != null)
            {
                return new StopModel(stop.Id, stop.Name, stop.Latitude, stop.Longitude);
            }
            return null;
        }
        public void DeleteStop(int id)
        {
            var stop = _userDb.Stop.FirstOrDefault(s => s.Id == id);
            if (stop != null)
            {
                _userDb.Stop.Remove(stop);
                _userDb.SaveChanges();
            }
        }
    }





}