using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rewards.Manager.DalContracts;
using Rewards.Manager.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Rewards.Manager.FileDal
{
    /// <summary>
    /// класс реализации интерфейса работы с данными
    /// </summary>
    public class BaseMedalDao:IMedalDao
    {
        string connectionString = @"Data Source=JDDEMON-HP ;Integrated Security=True;Initial Catalog = rewards";

        public bool Add(Medal medal)
        {
            // строка запроса обращается к хранимой процедуре
            string query = "exec AddMedal @Name='" + medal.Name + "',"
                            + "@Material = '" + medal.Material + "'";
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
            // строка запроса обращается к хранимой процедуре
            string query = "exec DeleteMedal @id=" + id.ToString();
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = query;
                command.ExecuteNonQuery();
            return true;
        }
        // получение всех элементов списка 
        public IEnumerable<Medal> GetAll()
        {
            // текст запроса-обращение к представлению
            string query = "select * from MedalView";
           
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                List<Medal> medals = new List<Medal>();
                foreach (DataRow row in table.Rows)
                {
                    Medal p = new Medal();
                    p.Id = int.Parse(row["MedalId"].ToString());
                    p.Name = row["MedalName"].ToString();
                    p.Material = row["MaterialName"].ToString();
                   
                    medals.Add(p);
                }
                return medals;
        }
        // получение элемента списка по идентификатору
        public Medal GetById(int id)
        {
            return GetAll().FirstOrDefault(medal => medal.Id == id);
        }
    }

}

