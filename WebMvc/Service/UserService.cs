using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            return _userDb.User.Select(u => new UserModel(u.Id, u.FirstName, u.LastName, u.UserName)).ToList();
        }

        public void CreateUser(string firstname, string lastname, string userName, string password)
        {
            var newUser = new Database.User
            {
                FirstName = firstname,
                LastName = lastname,
                UserName = userName,
                Password = HashPassword(password)
            };
            _userDb.User.Add(newUser);
            _userDb.SaveChanges();
        }

        public UserModel? FindUserByID(int id)
        {
            var user = _userDb.User.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                return new UserModel(user.Id, user.FirstName, user.LastName, user.UserName);
            }
            return null;
        }

        public bool VerifyUser(string userName, string password)
        {
            var user = _userDb.User.FirstOrDefault(u => u.UserName == userName);
            if (user != null && VerifyPassword(password, user.Password))
            {
                return true;
            }
            return false;
        }

        private string HashPassword(string password)
        {
            using (var hasher = new SHA256Managed())
            {
                return Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }

        private bool VerifyPassword(string providedPassword, string storedHash)
        {
            string providedHash = HashPassword(providedPassword);
            return providedHash == storedHash;
        }
    }
}
