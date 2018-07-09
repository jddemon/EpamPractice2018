using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rewards.Manager.Entities;
using Rewards.Manager.LogicContracts;
using Rewards.Manager.DalContracts;

namespace Rewards.Manager.Logic
{
    /// <summary>
    /// реализация уровня логики
    /// </summary>
    public class PersonLogic : IPersonLogic
    {

        private IPersonDao _personDao;

        public PersonLogic(IPersonDao personDao)
        {
            _personDao = personDao;
        }

        // получение списка персон
        public Person[] GetAll()
        {
            return _personDao.GetAll().ToArray();
        }

        // сохранение персоны
        public Person Save(Person person)
        {
            if (string.IsNullOrWhiteSpace(person.FirstName)
                || string.IsNullOrWhiteSpace(person.LastName)
                || string.IsNullOrWhiteSpace(person.City)
                || string.IsNullOrWhiteSpace(person.Street)
                || string.IsNullOrWhiteSpace(person.House)
                )
            {
                throw new ArgumentException("Note text cannot be null or whitespace.", nameof(person));
            }

            if (_personDao.Add(person))
            {
                return person;
            }
            else
                throw new InvalidOperationException("Error on person saving!");
        }

        // обновление персоны
        public Person Update(Person person)
        {
            if (string.IsNullOrWhiteSpace(person.FirstName)
                || string.IsNullOrWhiteSpace(person.LastName)
                || string.IsNullOrWhiteSpace(person.City)
                || string.IsNullOrWhiteSpace(person.Street)
                || string.IsNullOrWhiteSpace(person.House)
                )
            {
                throw new ArgumentException("Text cannot be null or whitespace.", nameof(person));
            }


            if (_personDao.Update(person))
            {
                return person;
            }
            else
                throw new InvalidOperationException("Error on person updating!");
        }

        // удаление из списка
        public bool Delete(int personId)
        {
            if(personId<=0)
            {
                throw new ArgumentException("Index may be > 0.", nameof(personId));
            }
            if (_personDao.Delete(personId))
            {
                return true;
            }
            else
                return false;
        }
    }
}
