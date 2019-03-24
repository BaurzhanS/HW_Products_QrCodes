using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HW_KoreanShop.Entities;

namespace HW_KoreanShop.Repositories
{
    public class UserRepository
    {
        private string GetTableName()
        {
            return $"[dbo].[users]";
        }

        public User Read(int id)
        {
            User user = new User();
            string sql = $"Select * from {GetTableName()} where id={id}";
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            user.Id = Int32.Parse(reader["Id"].ToString());
                            user.UserName = reader["UserName"].ToString();
                        }
                    }
                    else throw new Exception("No data found!");
                }
            }
            return user;
        }

        public IEnumerable<User> ReadAll()
        {
            List<User> users = new List<User>();
            string sql = $"Select * from {GetTableName()}";
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int Id = Int32.Parse(reader["Id"].ToString());
                            string UserName = reader["UserName"].ToString();

                            users.Add(new User()
                            {
                                Id = Id,
                                UserName = UserName
                            });
                        }
                        return users;
                    }
                    else throw new Exception("No data found!");
                }
            }
        }

        public void Insert(User user)
        {
            string sqlCommand = $"Insert into {GetTableName()} (UserName) " +
                $"Values (@userName)";
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, sqlConnection))
                {
                    SqlParameter userNameParam = new SqlParameter("@userName", user.UserName);

                    command.Parameters.Add(userNameParam);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            string sqlCommand = $"Delete from {GetTableName()} where Id=@id";

            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, sqlConnection))
                {
                    SqlParameter idParam = new SqlParameter("@id", id);

                    command.Parameters.Add(idParam);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(int id, User updated)
        {
            string sqlCommand = $"Update {GetTableName()} set UserName=@userName where Id=@id";

            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, sqlConnection))
                {
                    SqlParameter idParam = new SqlParameter("@id", id);
                    SqlParameter userNameParam = new SqlParameter("@userName", updated.UserName);

                    command.Parameters.Add(idParam);
                    command.Parameters.Add(userNameParam);

                    command.ExecuteNonQuery();
                }
            }
        }

        private string GetConnectionString()
        {
            ConnectionStringInAppConfig appConfig = new ConnectionStringInAppConfig();
            return appConfig.GetConnectionString();
        }
    }
    
}
