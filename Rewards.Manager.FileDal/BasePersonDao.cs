using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rewards.Manager.DalContracts;
using Rewards.Manager.Entities;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Rewards.Manager.FileDal
{
    /// <summary>
    /// класс реализует интерфейс доступа к данным
    /// </summary>
    public class BasePersonDao:IPersonDao
    {
        string connectionString = @"Data Source=JDDEMON-HP ;Integrated Security=True;Initial Catalog = rewards";

        public bool Add(Person person)
        {
            // строка запроса обращается к хранимой процедуре
            string query = "exec AddPerson @inn=" + person.INN + ","
                            + "@LastName = '" + person.LastName + "',"
                            + "@FirstName = '" + person.FirstName + "',"
                            + "@MiddleName = '" + person.MiddleName + "',"
                            + "@CityName = '" + person.City + "',"
                            + "@StreetName = '" + person.Street + "',"
                            + "@HOUSE = '" + person.House + "'";
          
              // создаем и открываем соединение с сервером
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();
                /// создаем и запускаем sql-команду
                SqlCommand command = new SqlCommand();
                command.Connection = connection;                   
                command.CommandText = query;
                command.ExecuteNonQuery();
            return true;
        }
        public IEnumerable<Person> GetAll()
        {
            // текст запроса-обращение к представлению
            string query = "select * from PersonView";
               SqlConnection connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = query;
                // запрос возвращает ридер для получения таблицы
                SqlDataReader reader=command.ExecuteReader();
                // для получения данных создаем таблицу
                DataTable table = new DataTable();
                //загружаем таблицу из ридера
                table.Load(reader);
                // выбираем данные из таблицы и создаем список персон
                List<Person> persons = new List<Person>();
                foreach (DataRow row in table.Rows)
                {
                    Person p = new Person();
                    p.Id = int.Parse(row["personId"].ToString());
                    p.INN = decimal.Parse(row["INN"].ToString());
                    p.LastName = row["LastName"].ToString();
                    p.FirstName = row["FirstName"].ToString();
                    p.MiddleName = row["MiddleName"].ToString();
                    p.City = row["CityName"].ToString();
                    p.Street = row["StreetName"].ToString();
                    p.House = row["House"].ToString();
                    persons.Add(p);
                }
                return persons;
        }
        // получение элемента списка по идентификатору
        public Person GetById(int id)
        {
            return GetAll().FirstOrDefault(person => person.Id == id);
        }

        public bool Update(Person person)
        {
            // обращение к хранимой процедуре UpdatePerson
            string query = "exec UpdatePerson @inn=" + person.INN + ","
                            + "@LastName = '" + person.LastName + "',"
                            + "@FirstName = '" + person.FirstName + "',"
                            + "@MiddleName = '" + person.MiddleName + "',"
                            + "@CityName = '" + person.City + "',"
                            + "@StreetName = '" + person.Street + "',"
                            + "@HOUSE = '" + person.House + "'";
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = query;
                command.ExecuteNonQuery();

            return true;
        }
        public bool Delete(int id)
        {
            // обращение к хранимой процедуре DeletePerson 
            string query = "exec DeletePerson @id=" + id.ToString();
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = query;
                command.ExecuteNonQuery();
            return true;
        }
    }
}
