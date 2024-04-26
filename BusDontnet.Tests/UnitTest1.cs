using System.Collections.Generic;
using System.Linq;
using Moq;
using WebMvc.Database;
using WebMvc.Service;
using Xunit;

namespace WebMvc.Tests
{
    public class BusServiceTests
    {
        [Fact]
        public void GetBusses_ReturnsListOfBusses()
        {
            // Arrange
            var mockUserDb = new Mock<UserDb>();
            var buses = new List
            {
                new Database.Bus { Id = 1, BusNumber = 101 },
                new Database.Bus { Id = 2, BusNumber = 102 },
                new Database.Bus { Id = 3, BusNumber = 103 }
            };
            mockUserDb.Setup(db => db.Bus).Returns(buses.AsQueryable());
            var busService = new BusService(mockUserDb.Object);

            // Act
            var result = busService.GetBusses();

            // Assert
            Assert.Equal(buses.Count, result.Count);
            for (int i = 0; i < buses.Count; i++)
            {
                Assert.Equal(buses[i].Id, result[i].Id);
                Assert.Equal(buses[i].BusNumber, result[i].BusNumber);
            }
        }
    }

    public class DriverServiceTests
    {
        [Fact]
        public void GetDrivers_ReturnsListOfDrivers()
        {
            // Arrange
            var mockUserDb = new Mock<UserDb>();
            var drivers = new List
            {
                new Database.Driver { Id = 1, FirstName = "John", LastName = "Doe" },
                new Database.Driver { Id = 2, FirstName = "Jane", LastName = "Smith" },
                new Database.Driver { Id = 3, FirstName = "Alice", LastName = "Johnson" }
            };
            mockUserDb.Setup(db => db.Driver).Returns(drivers.AsQueryable());
            var driverService = new DriverService(mockUserDb.Object);

            // Act
            var result = driverService.GetDrivers();

            // Assert
            Assert.Equal(drivers.Count, result.Count);
            for (int i = 0; i < drivers.Count; i++)
            {
                Assert.Equal(drivers[i].Id, result[i].Id);
                Assert.Equal(drivers[i].FirstName, result[i].FirstName);
                Assert.Equal(drivers[i].LastName, result[i].LastName);
            }
        }
    }

    public class EntryServiceTests
    {
        [Fact]
        public void GetEntries_ReturnsListOfEntries()
        {
            // Arrange
            var mockUserDb = new Mock<UserDb>();
            var mockRouteService = new Mock<RouteServiceInterface>();

            var entries = new List
            {
                new Entry { Id = 1, TimeStamp = DateTime.Now, Boarded = 10, LeftBehind = 0, BusId = 1, StopId = 1, DriverId = 1, LoopId = 1 },
                new Entry { Id = 2, TimeStamp = DateTime.Now, Boarded = 15, LeftBehind = 2, BusId = 2, StopId = 2, DriverId = 2, LoopId = 2 },
                new Entry { Id = 3, TimeStamp = DateTime.Now, Boarded = 8, LeftBehind = 1, BusId = 3, StopId = 3, DriverId = 3, LoopId = 3 }
            };
            mockUserDb.Setup(db => db.Entry).Returns(entries.AsQueryable());

            var entryService = new EntryService(mockUserDb.Object, mockRouteService.Object);

            // Act
            var result = entryService.GetEntries();

            // Assert
            Assert.Equal(entries.Count, result.Count);
            for (int i = 0; i < entries.Count; i++)
            {
                Assert.Equal(entries[i].Id, result[i].Id);
                Assert.Equal(entries[i].TimeStamp, result[i].TimeStamp);
                Assert.Equal(entries[i].Boarded, result[i].Boarded);
                Assert.Equal(entries[i].LeftBehind, result[i].LeftBehind);
                Assert.Equal(entries[i].BusId, result[i].BusId);
                Assert.Equal(entries[i].StopId, result[i].StopId);
                Assert.Equal(entries[i].DriverId, result[i].DriverId);
                Assert.Equal(entries[i].LoopId, result[i].LoopId);
            }
        }
    }

    public class LoopServiceTests
    {
        [Fact]
        public void GetLoops_ReturnsListOfLoops()
        {
            // Arrange
            var mockUserDb = new Mock<UserDb>();
            var loops = new List
            {
                new Database.Loop { Id = 1, Name = "Loop 1" },
                new Database.Loop { Id = 2, Name = "Loop 2" },
                new Database.Loop { Id = 3, Name = "Loop 3" }
            };
            mockUserDb.Setup(db => db.Loop).Returns(loops.AsQueryable());
            var loopService = new LoopService(mockUserDb.Object);

            // Act
            var result = loopService.GetLoops();

            // Assert
            Assert.Equal(loops.Count, result.Count);
            for (int i = 0; i < loops.Count; i++)
            {
                Assert.Equal(loops[i].Id, result[i].Id);
                Assert.Equal(loops[i].Name, result[i].Name);
            }
        }
    }

    public class RouteServiceTests
    {
        [Fact]
        public void GetRoutes_ReturnsListOfRoutes()
        {
            // Arrange
            var mockUserDb = new Mock<UserDb>();
            var routes = new List<Route>
            {
                new Route { Id = 1, Order = 1, StopId = 1, LoopId = 1 },
                new Route { Id = 2, Order = 2, StopId = 2, LoopId = 1 },
                new Route { Id = 3, Order = 3, StopId = 3, LoopId = 2 }
            };
            mockUserDb.Setup(db => db.Route).Returns(routes.AsQueryable());
            var routeService = new RouteService(mockUserDb.Object);

            // Act
            var result = routeService.GetRoutes();

            // Assert
            Assert.Equal(routes.Count, result.Count);
            for (int i = 0; i < routes.Count; i++)
            {
                Assert.Equal(routes[i].Id, result[i].Id);
                Assert.Equal(routes[i].Order, result[i].Order);
                Assert.Equal(routes[i].StopId, result[i].StopId);
                Assert.Equal(routes[i].LoopId, result[i].LoopId);
            }
        }
    }

    public class StopServiceTests
    {
        [Fact]
        public void GetStops_ReturnsListOfStops()
        {
            // Arrange
            var mockUserDb = new Mock<UserDb>();
            var stops = new List
            {
                new Database.Stop { Id = 1, Name = "Stop 1", Latitude = 123.456, Longitude = 456.789 },
                new Database.Stop { Id = 2, Name = "Stop 2", Latitude = 234.567, Longitude = 567.890 },
                new Database.Stop { Id = 3, Name = "Stop 3", Latitude = 345.678, Longitude = 678.901 }
            };
            mockUserDb.Setup(db => db.Stop).Returns(stops.AsQueryable());
            var stopService = new StopService(mockUserDb.Object);

            // Act
            var result = stopService.GetStops();

            // Assert
            Assert.Equal(stops.Count, result.Count);
            for (int i = 0; i < stops.Count; i++)
            {
                Assert.Equal(stops[i].Id, result[i].Id);
                Assert.Equal(stops[i].Name, result[i].Name);
                Assert.Equal(stops[i].Latitude, result[i].Latitude);
                Assert.Equal(stops[i].Longitude, result[i].Longitude);
            }
        }
    }

    public class UserServiceTests
    {
        [Fact]
        public void GetUsers_ReturnsListOfUsers()
        {
            // Arrange
            var mockUserDb = new Mock<UserDb>();
            var users = new List<User>
            {
                new User { Id = 1, FirstName = "John", LastName = "Doe", UserName = "john.doe", Password = "password" },
                new User { Id = 2, FirstName = "Jane", LastName = "Smith", UserName = "jane.smith", Password = "password" },
                new User { Id = 3, FirstName = "Alice", LastName = "Johnson", UserName = "alice.johnson", Password = "password" }
            };
            mockUserDb.Setup(db => db.User).Returns(users.AsQueryable());
            var userService = new UserService(mockUserDb.Object);

            // Act
            var result = userService.GetUsers();

            // Assert
            Assert.Equal(users.Count, result.Count);
            for (int i = 0; i < users.Count; i++)
            {
                Assert.Equal(users[i].Id, result[i].Id);
                Assert.Equal(users[i].FirstName, result[i].FirstName);
                Assert.Equal(users[i].LastName, result[i].LastName);
                Assert.Equal(users[i].UserName, result[i].UserName);
                Assert.Equal(users[i].Password, result[i].Password);
            }
        }
    }
}