using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HW_KoreanShop.Entities;

namespace HW_KoreanShop.Repositories
{
    public class QrCodeGeoRepository
    {
        private string GetTableName()
        {
            return $"[dbo].[qrCodesGeo]";
        }

        public QrCodeGeoEntity Read(int id)
        {
            QrCodeGeoEntity qrCode = new QrCodeGeoEntity();
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
                            qrCode.Id = Int32.Parse(reader["Id"].ToString());
                            qrCode.UserId = Int32.Parse(reader["UserId"].ToString());
                            qrCode.QrCodeType = (QrCodeType)reader["QrCodeType"];
                            qrCode.Content = (byte[])reader["Content"];
                        }
                    }
                    else throw new Exception("No data found!");
                }
            }
            return qrCode;
        }

        public IEnumerable<QrCodeGeoEntity> ReadAll()
        {
            List<QrCodeGeoEntity> qrCodes = new List<QrCodeGeoEntity>();
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
                            int UserId = Int32.Parse(reader["UserId"].ToString());
                            QrCodeType QrCodeType = (QrCodeType)reader["QrCodeType"];
                            byte[] Content = (byte[])reader["Content"];

                            qrCodes.Add(new QrCodeGeoEntity
                            {
                                Id = Id,
                                UserId = UserId,
                                QrCodeType = QrCodeType,
                                Content = Content
                            });
                        }
                        return qrCodes;
                    }
                    else throw new Exception("No data found!");
                }
            }

        }

        public void Insert(QrCodeGeoEntity qrCode)
        {
            string sqlCommand = $"Insert into {GetTableName()} (UserId, Content, QrCodeType) " +
                $"Values (@userId, @content, @qrCodeType)";
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, sqlConnection))
                {
                    SqlParameter userId = new SqlParameter("@userId", qrCode.UserId);
                    SqlParameter content = new SqlParameter("@content", qrCode.Content);
                    SqlParameter qrCodeType = new SqlParameter("@qrCodeType", (int)qrCode.QrCodeType);

                    command.Parameters.Add(userId);
                    command.Parameters.Add(content);
                    command.Parameters.Add(qrCodeType);

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

        public void Update(int id, QrCodeGeoEntity updated)
        {
            string sqlCommand = $"Update {GetTableName()} set UserId=@userId, QrCodeType=@qrCodeType, Content=@content where Id=@id";

            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, sqlConnection))
                {
                    SqlParameter userId = new SqlParameter("@userId", updated.UserId);
                    SqlParameter qrCodeType = new SqlParameter("@qrCodeType", (int)updated.QrCodeType);
                    SqlParameter content = new SqlParameter("@content", updated.Content);

                    command.Parameters.Add(userId);
                    command.Parameters.Add(qrCodeType);
                    command.Parameters.Add(content);

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
