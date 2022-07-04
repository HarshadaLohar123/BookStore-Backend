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
    public class CartRL:ICartRL
    {
        private SqlConnection sqlConnection;
        private readonly IConfiguration configuration;
        public CartRL(IConfiguration configuration)
        {
            this.configuration = configuration;

        }
        private IConfiguration Configuration { get; }
        public CartModel AddCart(CartModel cart, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                SqlCommand cmd = new SqlCommand("Addcart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BooksQuantity", cart.BooksQuantity);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@BookId", cart.BookId);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return cart;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }


        public string RemoveFromCart(int CartId)
        {
            sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
            try
            {

                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("RemoveFromCart", sqlConnection);//strore procedure name
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CartId", CartId);
              
                    sqlConnection.Open();
                    int result = cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (result != 0)
                    {
                        return "Book Removed";
                    }
                    else
                    {
                        return "Failed";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }


        public List<ViewCartModel> GetCartDetailsByUserid(int UserId)
        {
            sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("GetCartByUserId", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<ViewCartModel> cartmodels = new List<ViewCartModel>();
                        while (reader.Read())
                        {
                            BookModel bookModel = new BookModel();
                            ViewCartModel cartModel = new ViewCartModel();
                            bookModel.BookId = Convert.ToInt32(reader["BookId"]);
                            bookModel.BookName = reader["BookName"].ToString();
                            bookModel.AuthorName = reader["AuthorName"].ToString();
                            bookModel.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
                            bookModel.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                            bookModel.BookImage = reader["BookImage"].ToString();
                            cartModel.UserId = Convert.ToInt32(reader["UserId"]);
                            cartModel.BookId = Convert.ToInt32(reader["BookId"]);
                            cartModel.CartId = Convert.ToInt32(reader["CartId"]);
                            cartModel.BooksQuantity = Convert.ToInt32(reader["BooksQuantity"]);
                            cartModel.Bookmodel = bookModel;
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

        public CartModel UpdateCart(int CartId, CartModel cartModel, int UserId)
        {
            sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("UpdateCart", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BooksQuantity", cartModel.BooksQuantity);
                    cmd.Parameters.AddWithValue("@BookId", cartModel.BookId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@CartId", CartId);
                    sqlConnection.Open();
                    int result = cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (result != 0)
                    {
                        return cartModel;
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
