using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TechTest.Repositories.Models;
using TechTest.Repositories.Models.Responses;

namespace TechTest.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public PersonRepository()
        {
            this.Collection = InMemoryCollection;
        }

        private IList<Person> Collection { get; set; }

        public IEnumerable<Person> GetAll()
        {
            return this.Collection;
        }

        //changed from Person to PersonResponse so I can unit test each response type.
        public PersonResponse Get(int? id)
        {
            if (!id.HasValue)
            {
                return new PersonResponse
                {
                    statuscode = 400
                };

            }

            var person = this.Collection.SingleOrDefault(p => p.Id == id.Value);

            return new PersonResponse
            {
                Person = person,
                statuscode = person == null ? 204 : 200

            };
        }

        public PersonResponse Update(int? id, PersonUpdate personUpdate)
        {
            PersonResponse personResponse = new PersonResponse();

            if (personUpdate == null || id == null || personUpdate.Colours == null)
            {
                personResponse.statuscode = 400;
                return personResponse;
            }

            Person existingPerson;
            try
            {
                existingPerson = this.Collection.SingleOrDefault(p => p.Id == id.Value);
            }
            catch (Exception e)
            {
                //collection probably contains duplicate IDs
                //TODO we should log this exception
                throw;
            }


            if (existingPerson == null)
            {

                personResponse.statuscode = 204;
                return personResponse;
            }

            existingPerson.Authorised = personUpdate.Authorised;
            existingPerson.Colours = personUpdate.Colours;
            existingPerson.Enabled = personUpdate.Enabled;

            personResponse.Person = existingPerson;
            personResponse.statuscode = 200;

            return personResponse;
        }
        //TODO pass this in as a service so I can mock it
        private static IList<Person> InMemoryCollection { get; } = new List<Person>
            {
                new Person { Id = 1, FirstName = "Bo", LastName = "Bob", Authorised = true, Enabled = false, Colours = new List<Colour> { new Colour { Id = 1, Name = "Red" } } },
                new Person { Id = 2, FirstName = "Brian", LastName = "Allen", Authorised = true, Enabled = true, Colours = new List<Colour> { new Colour { Id = 1, Name = "Red" }, new Colour { Id = 2, Name = "Green" } , new Colour { Id = 3, Name = "Blue" } } },
                new Person { Id = 3, FirstName = "Courtney", LastName = "Arnold", Authorised = true, Enabled = true, Colours = new List<Colour> { new Colour { Id = 1, Name = "Red" } } },
                new Person { Id = 4, FirstName = "Gabriel", LastName = "Francis", Authorised = false, Enabled = false, Colours = new List<Colour> { new Colour { Id = 2, Name = "Green" } } },
                new Person { Id = 5, FirstName = "George", LastName = "Edwards", Authorised = true, Enabled = false, Colours = new List<Colour> { new Colour { Id = 2, Name = "Green" }, new Colour { Id = 3, Name = "Blue" } } },
                new Person { Id = 6, FirstName = "Imogen", LastName = "Kent", Authorised = false, Enabled = false, Colours = new List<Colour> { new Colour { Id = 1, Name = "Red" }, new Colour { Id = 2, Name = "Green" } } },
                new Person { Id = 7, FirstName = "Joel", LastName = "Daly", Authorised = true, Enabled = true, Colours = new List<Colour> { new Colour { Id = 2, Name = "Green" } } },
                new Person { Id = 8, FirstName = "Lilly", LastName = "Hale", Authorised = false, Enabled = false, Colours = new List<Colour> { new Colour { Id = 1, Name = "Red" }, new Colour { Id = 2, Name = "Green" } , new Colour { Id = 3, Name = "Blue" } } },
                new Person { Id = 9, FirstName = "Patrick", LastName = "Kerr", Authorised = true, Enabled = true, Colours = new List<Colour> { new Colour { Id = 2, Name = "Green" } } },
                new Person { Id = 10, FirstName = "Sharon", LastName = "Halt", Authorised = false, Enabled = false, Colours = new List<Colour> { new Colour { Id = 1, Name = "Red" }, new Colour { Id = 2, Name = "Green" } , new Colour { Id = 3, Name = "Blue" } } },
                new Person { Id = 11, FirstName = "Willis", LastName = "Tibbs", Authorised = true, Enabled = false, Colours = new List<Colour> { new Colour { Id = 1, Name = "Red" }, new Colour { Id = 2, Name = "Green" } , new Colour { Id = 3, Name = "Blue" } } },
            };
    }
}
