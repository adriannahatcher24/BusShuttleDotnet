using System;
using System.Collections.Generic;
using System.Linq;
using DomainModel;
using Microsoft.VisualBasic;
using WebMvc.Database;
namespace WebMvc.Service
{
    public class UserService : UserServiceInterface
    {
        private readonly userDb _userDb;

        public UserService(userDb userDb)
        {
            _userDb = userDb;
        }
        public List<UserModel> GetUsers()
        {
            var userList = _userDb.User.Select(u => new UserModel(u.Id, u.FirstName, u.LastName, u.UserName, u.Password)).ToList();
            return userList;
        }

        public void CreateUser(string firstname, string lastname, string userName, string password)
        {
            var newUser = new Database.User
            {
                FirstName = firstname,
                LastName = lastname,
                UserName = userName,
                Password = password
            };
            _userDb.User.Add(newUser);
            _userDb.SaveChanges();

        }


        public UserModel? FindUserByID(int id)
        {
            var user = _userDb.User.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                return new UserModel(user.Id, user.FirstName, user.LastName, user.UserName, user.Password);
            }
            return null;
        }
        public bool VerifyUserAsManager(string userName, string password)
        {
            var user = _userDb.User.FirstOrDefault(u => u.UserName == userName && u.Password == password);
            if (user != null && user.Id == 1)
            {
                return true;
            }
            return false;
        }
        public bool VerifyUser(string userName, string password)
        {
            var user = _userDb.User.FirstOrDefault(u => u.UserName == userName && u.Password == password);
            if (user != null)
            {
                return true;
            }
            return false;
        }
        public Driver VerifyUserAsDriver(string userName, string password)
        {
            var user = _userDb.User.FirstOrDefault(u => u.UserName == userName && u.Password == password);
            if (user != null && user.Id != 1)
            {
                var driver = _userDb.Driver.FirstOrDefault(d => d.FirstName == user.FirstName && d.LastName == user.LastName);
                if (driver != null)
                {
                    return driver;
                }
            }
            return null;
        }
    }
}