using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using PinCode_Master.Models;

namespace PinCode_Master.DAL
{
    public class Product_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["adoConnectionstring"].ToString();

        //Get All Products
        public List<Product> GetAllProducts()
        {
            List<Product> productList = new List<Product>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllProductts";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtProducts = new DataTable();

                connection.Open();
                sqlDA.Fill(dtProducts);
                connection.Close();

                foreach (DataRow dr in dtProducts.Rows)
                {
                    productList.Add(new Product
                    {
                         ID = Convert.ToInt32(dr["ID"]),
                        Pincode = dr["Pincode"].ToString(),
                        City = dr["City"].ToString(),
                        State = dr["State"].ToString(),
                        Country = dr["Country"].ToString(),
                        Active = dr["Active"] != DBNull.Value && Convert.ToBoolean(dr["Active"])
                    });
                }
            }
            return productList;
        }

        //Insert product data
        public int InsertProducts(Product product)
        {
            int Id = 0;

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_InsertProductts", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Pincode", product.Pincode);
                command.Parameters.AddWithValue("@City", product.City);
                command.Parameters.AddWithValue("@State", product.State);
                command.Parameters.AddWithValue("@Country", product.Country);
                command.Parameters.AddWithValue("@active", product.Active);

                connection.Open();
                Id = command.ExecuteNonQuery();
                connection.Close();
            }
            return Id;
        }

        //Get Products by ProductId
        public List<Product> GetProductsByID(int ID)
        {
            List<Product> productList = new List<Product>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetProducttByID";
                command.Parameters.AddWithValue("@ID", ID);
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtProducts = new DataTable();

                connection.Open();
                sqlDA.Fill(dtProducts);
                connection.Close();

                foreach (DataRow dr in dtProducts.Rows)
                {
                    productList.Add(new Product
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        Pincode = dr["Pincode"].ToString(),
                        City = dr["City"].ToString(),
                        State = dr["State"].ToString(),
                        Country = dr["Country"].ToString(),
                        Active = dr["Active"] != DBNull.Value && Convert.ToBoolean(dr["Active"])
                    });
                }
            }
            return productList;
        }


        //Update product data
        public int UpdateProducts(Product product)
        {
            int Id = 0;

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_UpdateProductts", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", product.ID);
                command.Parameters.AddWithValue("@Pincode", product.Pincode);
                command.Parameters.AddWithValue("@City", product.City);
                command.Parameters.AddWithValue("@State", product.State);
                command.Parameters.AddWithValue("@Country", product.Country);
                command.Parameters.AddWithValue("@active", product.Active);

                connection.Open();
                Id = command.ExecuteNonQuery();
                connection.Close();
            }
            return Id;
        }

        //Delete product data
        public int DeleteProducts(int ID)
        {
            int Id = 0;

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_DeleteProductt", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", ID);

                connection.Open();
                Id = command.ExecuteNonQuery();
                connection.Close();
            }
            return Id;
        }
    }
}