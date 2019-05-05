using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTest.Repositories;
using TechTest.Repositories.Models;

namespace TechTest.Tests.Repositories
{
    [TestClass]
    public class PersonRepositoryTests
    {
        [TestMethod]
        public void Get_Returns400WhenIdNotSupplied()
        {
            
            var uut = new PersonRepository();

            var result = uut.Get(null);

            Assert.AreEqual(400, result.statuscode);

        }

        [TestMethod]
        public void Get_Returns204WhenPersonNotFound()
        {
            var uut = new PersonRepository();

            //large number used that is unlikely to be added to the list
            var result = uut.Get(99999);

            Assert.AreEqual(204, result.statuscode);
        }

        [TestMethod]
        public void Get_Returns200WhenPersonIsFound()
        {
            var uut = new PersonRepository();

            //Id 1 used as it is likely that it will always be in the database.
            var result = uut.Get(1);

            Assert.AreEqual(200, result.statuscode);
        }

        [TestMethod]
        public void Update_Returns400WhenIdNotSupplied()
        {
            var uut = new PersonRepository();
            var personUpdate = new PersonUpdate
            {
                Colours = new List<Colour>()
            };


            var result = uut.Update(null, personUpdate);

            Assert.AreEqual(400, result.statuscode);
        }

        [TestMethod]
        public void Update_Returns400WhenPersonUpdateNotSupplied()
        {
            var uut = new PersonRepository();
            var personUpdate = new PersonUpdate
            {
                Colours = new List<Colour>()
            };


            var result = uut.Update(1, null);

            Assert.AreEqual(400, result.statuscode);
        }

        [TestMethod]
        public void Update_Returns200WhenColourNotSupplied()
        {
            var uut = new PersonRepository();
            var personUpdate = new PersonUpdate
            {
                Colours = new List<Colour>()
            };

            var result = uut.Update(1, personUpdate);

            Assert.AreEqual(200, result.statuscode);
        }
    }
}
