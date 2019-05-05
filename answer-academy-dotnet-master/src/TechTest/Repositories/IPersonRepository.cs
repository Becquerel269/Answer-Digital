using System.Collections.Generic;
using TechTest.Repositories.Models;
using TechTest.Repositories.Models.Responses;

namespace TechTest.Repositories
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetAll();

        PersonResponse Get(int? id);
        

        Person Update(Person person);
    }
}
