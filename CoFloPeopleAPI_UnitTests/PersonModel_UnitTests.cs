using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoFloPeopleAPI_UnitTests
{
    [TestClass]
    public class PersonModel_UnitTests
    {
        [TestMethod]
        public void PersonModelConstrAgeStd_Success()
        {
            // Arrange
            // Input:
            var fName = "Piet";
            var sName = "Odendaal";
            DateTime birthDate = DateTime.Now.AddYears(-15).AddDays(-10);

            // Expected Output:
            int eAge = 15;

            // Act
            var tPersonModel = new PersonModel()
            {
                FirstName = fName,
                LastName = sName,
                BirthDate = birthDate
            };

            // Assert
            Assert.AreEqual(eAge, tPersonModel.Age);
        }

        [TestMethod]
        public void PersonModelConstrAgeSameDay_Success()
        {
            // Arrange
            // Input:
            var fName = "Piet";
            var sName = "Odendaal";
            DateTime birthDate = DateTime.Now.AddYears(-15);

            // Expected Output:
            int eAge = 15;

            // Act
            var tPersonModel = new PersonModel()
            {
                FirstName = fName,
                LastName = sName,
                BirthDate = birthDate
            };

            // Assert
            Assert.AreEqual(eAge, tPersonModel.Age);

        }

        [TestMethod]
        public void PersonModelConstrAgeNextDay_Success()
        {
            // Arrange
            // Input:
            var fName = "Piet";
            var sName = "Odendaal";
            DateTime birthDate = DateTime.Now.AddYears(-15).AddDays(1);

            // Expected Output:
            int eAge = 14;

            // Act
            var tPersonModel = new PersonModel()
            {
                FirstName = fName,
                LastName = sName,
                BirthDate = birthDate
            };

            // Assert
            Assert.AreEqual(eAge, tPersonModel.Age);

        }
    }
}
