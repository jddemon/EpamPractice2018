using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rewards.Manager.Entities;

namespace Rewards.Manager.DalContracts
{
    /// <summary>
    /// интерфейс получения данных для Person
    /// </summary>
    public interface IPersonDao
    {
        bool Add(Person person);
        IEnumerable<Person> GetAll();
        Person GetById(int id);
        bool Update(Person person);
        bool Delete(int id);
    }
}
