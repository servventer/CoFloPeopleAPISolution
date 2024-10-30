using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using FluentAssertions;

namespace CoFloPeopleAPI_UnitTests
{
    [TestClass]
    public class PeopleManagement_UnitTests
    {
        private static PeopleManagement _peopleManagement;
        private readonly Mock<ILogger<PeopleManagement>> _logger = new Mock<ILogger<PeopleManagement>>();
        private CoFloPeopleAPIContext _context;
        private IMapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            // Configure AutoMapper profile
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfilePersonModel());
            });
            _mapper = mappingConfig.CreateMapper();

            // Create a mock data set with sample data
            var options = new DbContextOptionsBuilder<CoFloPeopleAPIContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;

            _context = new CoFloPeopleAPIContext(options);
            _context.PersonModel.Add(new PersonModelDB
            {
                Id = 1,
                FirstName = "Peter",
                LastName = "Dempsey",
                BirthDate = new DateTime(1980, 5, 25)
            });
            _context.PersonModel.Add(new PersonModelDB
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Luck",
                BirthDate = new DateTime(1955, 8, 5)
            });
            _context.SaveChanges();

            // Instance of PeopleManagement
            _peopleManagement = new PeopleManagement(_logger.Object, _context, _mapper);

        }

        [TestMethod]
        public async Task GetPersonById_Success()
        {
            // Arrange
            // Input:
            int id = 2;

            // Expected Output:
            PersonModel exOutPerson = new PersonModel()
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Luck",
                BirthDate = DateTime.Parse("1955-08-05")
            };

            // Act
            var outPerson = await _peopleManagement.GetPersonById(id);

            // Assert
            Assert.AreEqual(exOutPerson.Age, outPerson.Age);
        }

        [TestMethod]
        public async Task GetPersonById_Fail()
        {
            // Arrange
            // Input:
            int id = 3;

            // Expected Output:
            string errorOut = "Person with ID : 3 not found";
            string actOut = "";

            // Act
            try
            {
                var outPerson = await _peopleManagement.GetPersonById(id);
            }
            catch (Exception e)
            {
                actOut = e.Message;
            }

            // Assert
            Assert.AreEqual(actOut, errorOut);
        }

        [TestMethod]
        public async Task UpdatePersonAsync_Success()
        {
            // Arrange
            // Input:
            PersonModel inPerson = new PersonModel()
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Brown",
                BirthDate = DateTime.Parse("1955-08-05")
            };
            // Expect Output:
            var exOutLastname = "Brown";

            // Act
            var outPerson = await _peopleManagement.UpdatePersonAsync(inPerson);

            // Assert
            Assert.AreEqual(outPerson.LastName, exOutLastname);

        }

        [TestMethod]
        public async Task GetListOfPersonAsync_Success()
        {
            // Arrange
            // Input:
            // Expected Output:
            List<PersonModel> exOutPeople = new List<PersonModel>();
            exOutPeople.Add(new PersonModel()
            {
                Id = 1,
                FirstName = "Peter",
                LastName = "Dempsey",
                BirthDate = DateTime.Parse("1980-05-25")

            });
            exOutPeople.Add(new PersonModel()
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Luck",
                BirthDate = DateTime.Parse("1955-08-05")

            });

            // Act
            var outPeople = await _peopleManagement.GetListOfPersonAsync();
            var firstPerson = outPeople.FirstOrDefault();

            // Assert
            Assert.AreEqual(firstPerson.FirstName, exOutPeople[0].FirstName);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
        }

    }
}
