using System;
using System.Collections.Generic;
using System.Linq;
using DomainModel;
using Microsoft.VisualBasic;
using WebMvc.Database;
namespace WebMvc.Service
{
    public class DriverService : DriverServiceInterface
    {
        private readonly userDb _userDb;

        public DriverService(userDb userDb)
        {
            _userDb = userDb;
        }
        public List<DriverModel> GetDrivers()
        {
            var driverList = _userDb.Driver.Select(d => new DriverModel(d.Id, d.FirstName, d.LastName)).ToList();
            return driverList;
        }

        public void UpdateDriverByID(int id, string firstname, string lastname)
        {
            var driver = _userDb.Driver.FirstOrDefault(d => d.Id == id);
            if (driver != null)
            {
                driver.FirstName = firstname;
                driver.LastName = lastname;
                _userDb.SaveChanges();

            }
        }
        public void CreateDriver(string firstname, string lastname)
        {
            var newDriver = new Database.Driver
            {
                FirstName = firstname,
                LastName = lastname
            };
            _userDb.Driver.Add(newDriver);
            _userDb.SaveChanges();

        }


        public DriverModel? FindDriverByID(int id)
        {
            var driver = _userDb.Driver.FirstOrDefault(d => d.Id == id);
            if (driver != null)
            {
                return new DriverModel(driver.Id, driver.FirstName, driver.LastName);
            }
            return null;
        }
        public void DeleteDriver(int id)
        {
            var driver = _userDb.Driver.FirstOrDefault(d => d.Id == id);
            if (driver != null)
            {
                _userDb.Driver.Remove(driver);
                _userDb.SaveChanges();
            }
        }

        public int GetDriverByName(string fullName)
        {
            string[] nameParts = fullName.Split(' ');

            if (nameParts.Length >= 2)
            {
                var fName = nameParts[0];
                var lName = nameParts[1];

                Console.WriteLine($"First Name: {fName}");
                Console.WriteLine($"Last Name: {lName}");

                var driver = _userDb.Driver.FirstOrDefault(d => d.FirstName == fName && d.LastName == lName);
                if (driver != null)
                {
                    Console.WriteLine($"Driver ID: {driver.Id}");
                    return driver.Id;
                }
                else
                {
                    Console.WriteLine("Driver not found.");
                    return 0;
                }
            }
            else
            {
                Console.WriteLine("Invalid name format.");
                return 0; // Handle error or invalid name format
            }
        }

    }
}