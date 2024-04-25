using System;
using System.Collections.Generic;
using System.Linq;
using DomainModel;
using Microsoft.VisualBasic;
using WebMvc.Database;
namespace WebMvc.Service
{
    public class LoopService : LoopServiceInterface
    {
        private readonly userDb _userDb;

        public LoopService(userDb userDb)
        {
            _userDb = userDb;
        }
        public List<LoopModel> GetLoops()
        {
            var loopList = _userDb.Loop.Select(l => new LoopModel(l.Id, l.Name)).ToList();
            return loopList;
        }

        public void UpdateLoopByID(int id, string name)
        {
            var loop = _userDb.Loop.FirstOrDefault(l => l.Id == id);
            if (loop != null)
            {
                loop.Name = name;
                _userDb.SaveChanges();

            }
        }
        public void CreateLoop(string name)
        {
            var newLoop = new Database.Loop
            {
                Name = name
            };
            _userDb.Loop.Add(newLoop);
            _userDb.SaveChanges();
        }

        public LoopModel? FindLoopByID(int id)
        {
            var loop = _userDb.Loop.FirstOrDefault(l => l.Id == id);
            if (loop != null)
            {
                return new LoopModel(loop.Id, loop.Name);
            }
            return null;
        }
        public void DeleteLoop(int id)
        {
            var loop = _userDb.Loop.FirstOrDefault(l => l.Id == id);
            if (loop != null)
            {
                _userDb.Loop.Remove(loop);
                _userDb.SaveChanges();
            }
        }
    }
}