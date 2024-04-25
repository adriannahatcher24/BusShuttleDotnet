using System;
using System.Collections.Generic;
using System.Linq;
using DomainModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using WebMvc.Database;
namespace WebMvc.Service
{
    public class EntryService : EntryServiceInterface
    {
        private readonly userDb _userDb;
        private readonly RouteServiceInterface routeService;

        public EntryService(userDb userDb, RouteServiceInterface routeServiceInterface)
        {
            _userDb = userDb;
            routeService = routeServiceInterface;
        }
        public List<EntryModel> GetEntries()
        {
            var entryList = _userDb.Entry.Select(e => new EntryModel(e.Id, e.TimeStamp, e.Boarded, e.LeftBehind, e.StopId, e.StopId, e.DriverId, e.LoopId)).ToList();
            return entryList;
        }

        public void UpdateEntryByID(int id, DateTime timeStamp, int boarded, int leftBehind)
        {
            var entry = _userDb.Entry.FirstOrDefault(e => e.Id == id);
            if (entry != null)
            {
                entry.TimeStamp = timeStamp;
                entry.Boarded = boarded;
                entry.LeftBehind = leftBehind;
                _userDb.SaveChanges();

            }
        }
        public void CreateEntry(DateTime timeStamp, int boarded, int leftBehind, int busId, int stopId, int driverId, int loopId)
        {
            var newEntry = new Database.Entry
            {
                TimeStamp = timeStamp,
                Boarded = boarded,
                LeftBehind = leftBehind,
                BusId = busId,
                LoopId = loopId,
                DriverId = driverId,
                StopId = stopId

            };
            _userDb.Entry.Add(newEntry);
            _userDb.SaveChanges();

        }

        public EntryModel? FindEntryByID(int id)
        {
            var entry = _userDb.Entry.FirstOrDefault(e => e.Id == id);
            if (entry != null)
            {
                return new EntryModel(entry.Id, entry.TimeStamp, entry.Boarded, entry.LeftBehind, entry.BusId, entry.StopId, entry.DriverId, entry.LoopId);
            }
            return null;
        }
        public void DeleteEntry(int id)
        {
            var entry = _userDb.Entry.FirstOrDefault(e => e.Id == id);
            if (entry != null)
            {
                _userDb.Entry.Remove(entry);
                _userDb.SaveChanges();
            }
        }

        public class EntryDetailDTO
        {
            public int Id { get; set; }
            public DateTime TimeStamp { get; set; }
            public int Boarded { get; set; }
            public int LeftBehind { get; set; }
            public int StopId { get; set; }
            public string StopName { get; set; }
            public int LoopId { get; set; }
            public string LoopName { get; set; }
            public int DriverId { get; set; }
            public string DriverName { get; set; }
            public int BusId { get; set; }
            public int BusNumber { get; set; }
        }

        public List<EntryDetailDTO> GetEntryDetails()
        {
            var entryDetails = _userDb.Entry
                .Include(r => r.Stop) // Ensure your Route entity has navigation properties to Stop and Loop
                .Include(r => r.Loop)
                .Include(r => r.Driver)
                .Include(r => r.Bus)
                .Select(r => new EntryDetailDTO
                {
                    Id = r.Id,
                    TimeStamp = r.TimeStamp,
                    Boarded = r.Boarded,
                    LeftBehind = r.LeftBehind,
                    StopId = r.StopId,
                    StopName = r.Stop.Name,
                    LoopId = r.LoopId, // Assuming Stop has a Name property
                    LoopName = r.Loop.Name,
                    DriverId = r.DriverId, // Assuming Loop has a Name property
                    DriverName = r.Driver.FirstName + " " + r.Driver.LastName,
                    BusId = r.BusId,
                    BusNumber = r.Bus.BusNumber
                }).ToList();

            return entryDetails;
        }
        public class EntryDetailsWithLoopDTO
        {
            public int Id { get; set; }
            public string StopName { get; set; }
        }
        public List<EntryDetailsWithLoopDTO> GetAvailableStops(int loopId)
        {
            var routes = routeService.GetRouteDetailsByLoop(loopId)
            .Select(r => new EntryDetailsWithLoopDTO
            {
                Id = r.StopId,
                StopName = r.StopName // Assuming Stop has a Name property
            }).ToList();
            return routes;
        }
    }
}