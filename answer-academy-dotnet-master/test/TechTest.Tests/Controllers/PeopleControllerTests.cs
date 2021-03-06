using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TechTest.Controllers;
using TechTest.Repositories;
using TechTest.Repositories.Models;
using TechTest.Repositories.Models.Responses;

namespace TechTest.Tests.Controllers
{
    [TestClass]
    public class PeopleControllerTests
    {

        //This Unit test test implementation not business logic
        //[TestMethod]
        //public void GetAll_Calls_Repository()
        //{
        //    // Arrange
        //    var mockRepo = new Mock<IPersonRepository>();
        //    var t = GetTestPeople().AsEnumerable();
        //    mockRepo.Setup(repo => repo.GetAll()).Returns(t);
        //    var controller = new PeopleController(mockRepo.Object);

        //    // Act
        //    controller.GetAll();

        //    // Assert
        //    mockRepo.Verify(mock => mock.GetAll(), Times.Once());
        //}

        [TestMethod]
        public void GetAll_Returns_Ok()
        {
            // Arrange
            var mockRepo = new Mock<IPersonRepository>();
            mockRepo.Setup(repo => repo.GetAll()).Returns(GetTestPeople().AsEnumerable());
            var controller = new PeopleController(mockRepo.Object);

            // Act
            var result = controller.GetAll();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetAll_Returns_People()
        {
            // Arrange
            var mockRepo = new Mock<IPersonRepository>();
            var people = GetTestPeople();
            mockRepo.Setup(repo => repo.GetAll()).Returns(people.AsEnumerable());
            var controller = new PeopleController(mockRepo.Object);
          

            // Act
            var result = controller.GetAll();
            var objectResult = result as OkObjectResult;
            var value = objectResult?.Value as IEnumerable<Person>;

            // Assert
            Assert.IsInstanceOfType(value, typeof(IEnumerable<Person>));
            Assert.AreEqual(people.Count, value?.Count());
        }

        [TestMethod]
        public void GetAll_Returns_Empty_List()
        {
            // Arrange
            var mockRepo = new Mock<IPersonRepository>();
            mockRepo.Setup(repo => repo.GetAll()).Returns(EmptyList);
            var controller = new PeopleController(mockRepo.Object);

            // Act
            var result = controller.GetAll();
            var objectResult = result as OkObjectResult;
            var value = objectResult?.Value as IEnumerable<Person>;

            // Assert
            Assert.IsInstanceOfType(value, typeof(IEnumerable<Person>));
            Assert.AreEqual(0, value?.Count());
        }

        //This Unit test test implementation not business logic
        //[TestMethod]
        //public void Get_Calls_Repository()
        //{
        //    // Arrange
        //    var mockRepo = new Mock<IPersonRepository>();
        //    var personId = 1;
        //    var personresponse = new PersonResponse
        //    {
        //        Person = GetTestPeople().FirstOrDefault(p => p.Id == personId),
        //        statuscode = 200
        //    };

        //    mockRepo.Setup(repo => repo.Get(personId))
        //            .Returns(personresponse);

        //    var controller = new PeopleController(mockRepo.Object);

        //    // Act
        //    controller.Get(personId);

        //    // Assert
        //    mockRepo.Verify(mock => mock.Get(personId), Times.Once());
        //}

        [TestMethod]
        public void Get_Returns_Ok()
        {
            // Arrange
            var mockRepo = new Mock<IPersonRepository>();
            var personId = 1;
            var personresponse = new PersonResponse
            {
                Person = GetTestPeople().FirstOrDefault(p => p.Id == personId),
                statuscode = 200
            };

            mockRepo.Setup(repo => repo.Get(personId))
                    .Returns(personresponse);

            var controller = new PeopleController(mockRepo.Object);

            // Act
            var result = controller.Get(personId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void Get_Returns_Person()
        {
            // Arrange
            var mockRepo = new Mock<IPersonRepository>();
            var personId = 1;
            
            var personresponse = new PersonResponse
            {
                Person = GetTestPeople().FirstOrDefault(p => p.Id == personId),
                statuscode = 200
            };
            mockRepo.Setup(repo => repo.Get(personId))
                    .Returns(personresponse);

            var controller = new PeopleController(mockRepo.Object);

            // Act
            var result = controller.Get(personId);
            var objectResult = result as OkObjectResult;
            var value = objectResult?.Value as Person;

            // Assert
            Assert.IsInstanceOfType(objectResult?.Value, typeof(Person));
            Assert.AreEqual(personresponse?.Person?.Id, value?.Id);
            Assert.AreEqual(personresponse?.Person?.FirstName, value?.FirstName);
            Assert.AreEqual(personresponse?.Person?.LastName, value?.LastName);
        }

        [TestMethod]
        public void Get_Returns_NotFound()
        {
            // Arrange
            var mockRepo = new Mock<IPersonRepository>();
            var personId = 100;
            var personresponse = new PersonResponse
            {
                Person = GetTestPeople().FirstOrDefault(p => p.Id == personId),
                statuscode = 200
            };

            mockRepo.Setup(repo => repo.Get(personId))
                    .Returns(null as PersonResponse);

            var controller = new PeopleController(mockRepo.Object);

            // Act
            var result = controller.Get(personId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        //This Unit test test implementation not business logic
        //[TestMethod]
        //public void Update_Calls_Repository()
        //{
        //    // Arrange
        //    var mockRepo = new Mock<IPersonRepository>();
        //    var personId = 1;
        //    var personUpdate = GetPersonUpdate();
            

        //    var personresponse = new PersonResponse
        //    {
        //        Person = GetTestPeople().FirstOrDefault(p => p.Id == personId),
        //        statuscode = 200
        //    };
        //    GetMocks(mockRepo, personId, personresponse);

        //    var controller = new PeopleController(mockRepo.Object);

        //    // Act
        //    controller.Update(personId, personUpdate);

        //    // Assert
        //    mockRepo.Verify(mock => mock.Get(personId), Times.Once());
        //    mockRepo.Verify(mock => mock.Update(null, personresponse.Person), Times.Once());
        //}

        [TestMethod]
        public void Update_Returns_Ok()
        {
            // Arrange
            var mockRepo = new Mock<IPersonRepository>();
            var personId = 1;
            var personUpdate = GetPersonUpdate();
            var personresponse = new PersonResponse
            {
                Person = GetTestPeople().FirstOrDefault(p => p.Id == personId),
                statuscode = 200
            };
            MockRepoGet(mockRepo, personId, personresponse);
            MockRepoUpdate(mockRepo, personId, personresponse, personUpdate);

            var controller = new PeopleController(mockRepo.Object);

            // Act
            var result = controller.Update(personId, personUpdate);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        private static void MockRepoGet(Mock<IPersonRepository> mockRepo, int personId, PersonResponse personresponse)
        {
            mockRepo.Setup(repo => repo.Get(personId))
                                .Returns(personresponse);

        }

        private static void MockRepoUpdate(Mock<IPersonRepository> mockRepo, int personId, PersonResponse personresponse, PersonUpdate personUpdate)
        {

            mockRepo.Setup(repo => repo.Update(personId, personUpdate))
                    .Returns(personresponse);
        }

      

        [TestMethod]
        public void Update_Returns_Person()
        {
            // Arrange
            var mockRepo = new Mock<IPersonRepository>();
            var personId = 1;
            var personUpdate = GetPersonUpdate();
            var person = GetTestPeople().FirstOrDefault(p => p.Id == personId);
            var updatedPerson =
                new Person
                {
                    Id = personId,
                    Authorised = personUpdate.Authorised,
                    Colours = personUpdate.Colours,
                    Enabled = personUpdate.Enabled,
                    FirstName = person?.FirstName,
                    LastName = person?.LastName
                };
            var personresponse = new PersonResponse
            {
                Person = GetTestPeople().FirstOrDefault(p => p.Id == personId),
                statuscode = 200
            };
            MockRepoGet(mockRepo, personId, personresponse);
            MockRepoUpdate(mockRepo, personId, personresponse, personUpdate);

            var controller = new PeopleController(mockRepo.Object);

            // Act
            var result = controller.Update(personId, personUpdate);
            var objectResult = result as OkObjectResult;
            var value = objectResult?.Value as Person;

            // Assert
            Assert.IsInstanceOfType(value, typeof(Person));
            Assert.AreEqual(updatedPerson.Id, value?.Id);
            Assert.AreEqual(updatedPerson.Authorised, value?.Authorised);
            Assert.AreEqual(updatedPerson.Enabled, value?.Enabled);
            Assert.IsTrue(updatedPerson.Colours.All(c => value?.Colours.Select(x => x.Id).Contains(c.Id) == true), "Colours do not match.");
        }

        [TestMethod]
        public void Update_Returns_NotFound()
        {
            var mockRepo = new Mock<IPersonRepository>();
            var personId = 100;
            var personUpdate = GetPersonUpdate();
            mockRepo.Setup(repo => repo.Get(personId)).Returns(null as PersonResponse);
            mockRepo.Setup(repo => repo.Update(It.IsAny<int?>(), It.IsAny<PersonUpdate>())).Returns(null as PersonResponse);
            var controller = new PeopleController(mockRepo.Object);
    
            var result = controller.Update(personId, personUpdate);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private static IList<Person> GetTestPeople()
        {
            var people = new List<Person>
            {
                new Person { Id = 1, FirstName = "Bo", LastName = "Bob", Authorised = true, Enabled = false, Colours = new List<Colour> { new Colour { Id = 1, Name = "Red" } } },
                new Person { Id = 2, FirstName = "Brian", LastName = "Allen", Authorised = true, Enabled = true, Colours = new List<Colour> { new Colour { Id = 1, Name = "Red" }, new Colour { Id = 2, Name = "Green" } , new Colour { Id = 3, Name = "Blue" } } },
            };

            return people;
        }

        private static IEnumerable<Person> EmptyList => new List<Person>();

        private static PersonUpdate GetPersonUpdate() => new PersonUpdate
        {
            Authorised = false,
            Enabled = true,
            Colours = new List<Colour> { new Colour { Id = 2, Name = "Green" } }
        };
    }
}
