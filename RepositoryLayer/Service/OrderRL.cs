using DatabaseLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Service
{
    public class OrderRL:IOrderRL
    {
        private SqlConnection sqlConnection;
        private readonly IConfiguration configuration;
        public OrderRL(IConfiguration configuration)
        {
            this.configuration = configuration;

        }
        public string AddOrder(OrderModel orderModel, int userId)
        {
            this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);

            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("AddOrders", this.sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookQuantity", orderModel.BookQuantity);
                    cmd.Parameters.AddWithValue("@BookId", orderModel.BookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@AddressId", orderModel.AddressId);
                    sqlConnection.Open();
                    int result = cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (result == 2)
                    {
                        return "Please Enter Correct Address TypeId For Adding Address";
                    }
                    else
                    {
                        return "Address Added Successfully";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ViewOrderModel> GetAllOrder(int userId)
        {
            this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("GetAllOrders", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<ViewOrderModel> cartmodels = new List<ViewOrderModel>();
                        while (reader.Read())
                        {

                            ViewOrderModel cartModel = new ViewOrderModel();
                            cartModel.BookId = Convert.ToInt32(reader["BookId"]);
                            cartModel.BookName = reader["BookName"].ToString();
                            cartModel.AuthorName = reader["AuthorName"].ToString();
                            //cartModel.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
                            cartModel.OrderDateTime = Convert.ToDateTime(reader["OrderDate"] == DBNull.Value ? default : reader["OrderDate"]);
                            cartModel.OrderDate = cartModel.OrderDateTime.ToString("dd-MM-yyyy");
                            cartModel.TotalPrice = Convert.ToInt32(reader["TotalPrice"]);
                            cartModel.BookImage = reader["BookImage"].ToString();
                            cartModel.UserId = Convert.ToInt32(reader["UserId"]);
                            cartModel.AddressId = Convert.ToInt32(reader["AddressId"]);
                            cartModel.OrderId = Convert.ToInt32(reader["OrderId"]);
                            cartModel.BookQuantity = Convert.ToInt32(reader["BookQuantity"]);
                            //cartModel.AddBookModel = bookModel;
                            cartmodels.Add(cartModel);
                        }

                        sqlConnection.Close();
                        return cartmodels;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;

            }
        }
    }
}
