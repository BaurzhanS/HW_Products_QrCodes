using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using HW_KoreanShop.Entities;
using HW_KoreanShop;


namespace HW_KoreanShop.Repositories
{
    public class ProductRepository
    {
        private string GetTableName()
        {
            return $"[dbo].[products]";
        }

        public Product Read(int id)
        {
            Product product = new Product();
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
                            product.Id = Int32.Parse(reader["Id"].ToString());
                            product.ProductName = reader["ProductName"].ToString();
                            product.Cost = Decimal.Parse(reader["Cost"].ToString());
                            product.Currency = reader["Currency"].ToString();
                            product.CostInTenge = decimal.Parse(reader["CostInTenge"].ToString());
                        }
                    }
                    else throw new Exception("No data found!");
                }
            }
            return product;
        }

        public IEnumerable<Product> ReadAll()
        {
            List<Product> products = new List<Product>();
            string sql = $"Select * from {GetTableName()}";
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sql,sqlConnection))
                {
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int Id = Int32.Parse(reader["Id"].ToString());
                            string ProductName = reader["ProductName"].ToString();
                            decimal Cost = Decimal.Parse(reader["Cost"].ToString());
                            string Currency = reader["Currency"].ToString();
                            decimal CostInTenge = decimal.Parse(reader["CostInTenge"].ToString());

                            products.Add(new Product()
                            {
                                Id = Id,
                                ProductName = ProductName,
                                Cost = Cost,
                                Currency = Currency,
                                CostInTenge = CostInTenge
                            });
                        }
                        return products;
                    }
                    else throw new Exception("No data found!");
                }
            }
        }

        public void Insert(Product product)
        {
            string sqlCommand = $"Insert into {GetTableName()} (ProductName,Cost,Currency,CostInTenge) " +
                $"Values (@productName, @cost, @currency, @costInTenge)";
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, sqlConnection))
                {
                    SqlParameter productNameParam = new SqlParameter("@productName", product.ProductName);
                    SqlParameter costParam = new SqlParameter("@cost", product.Cost);
                    SqlParameter currencyParam = new SqlParameter("@currency", product.Currency);
                    SqlParameter costInTengeParam = new SqlParameter("@costInTenge", product.CostInTenge);

                    command.Parameters.Add(productNameParam);
                    command.Parameters.Add(costParam);
                    command.Parameters.Add(currencyParam);
                    command.Parameters.Add(costInTengeParam);

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

        public void Update(int id, Product updated)
        {

            string sqlCommand = $"Update {GetTableName()} set Productname=@productName, Cost=@cost where Id=@id";

            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, sqlConnection))
                {
                    SqlParameter idParam = new SqlParameter("@id", id);
                    SqlParameter productNameParam = new SqlParameter("@productName", updated.ProductName);
                    SqlParameter costParam = new SqlParameter("@cost", updated.Cost);
                    SqlParameter currencyParam = new SqlParameter("@cost", updated.Currency);
                    SqlParameter costInTengeParam = new SqlParameter("@cost", updated.CostInTenge);

                    command.Parameters.Add(idParam);
                    command.Parameters.Add(productNameParam);
                    command.Parameters.Add(costParam);
                    command.Parameters.Add(currencyParam);
                    command.Parameters.Add(costInTengeParam);


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
