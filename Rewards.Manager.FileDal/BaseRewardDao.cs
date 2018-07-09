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
    public class BaseRewardDao:IRewardDao
    {
        string connectionString = @"Data Source=JDDEMON-HP ;Integrated Security=True;Initial Catalog = rewards";

        public bool Add(Reward reward)
        {
            // строка запроса обращается к хранимой процедуре
            string query = "exec AddReward @INN=" + reward.INN.ToString() + ","
                            + "@Medal = '" + reward.Medal + "'";
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = query;
                command.ExecuteNonQuery();
            return true;
        }

        public IEnumerable<Reward> GetPersonAll(int id)
        {
            // текст запроса-обращение к представлению
            string query = "select * from RewardView where persoiId="+id.ToString();
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                List<Reward> rewards = new List<Reward>();
                foreach (DataRow row in table.Rows)
                {
                    Reward p = new Reward();
                    p.INN = decimal.Parse(row["INN"].ToString());
                    p.Medal = row["MedalName"].ToString();
                    p.FirstName = row["FirstName"].ToString();
                    p.LastName = row["LastName"].ToString();

                    rewards.Add(p);
                }
                return rewards;
        }
    }
}
