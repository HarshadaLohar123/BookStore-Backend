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
    public class BookRL : IBookRL
    {
        private SqlConnection sqlConnection;
        private readonly IConfiguration configuration;
        public BookRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        private IConfiguration Configuration { get; }

        public BookModel AddBook(BookModel book)
        {
            sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
            try
            {

                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("AddBook", sqlConnection);//strore procedure name
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookName", book.BookName);
                    cmd.Parameters.AddWithValue("@authorName", book.AuthorName);
                    cmd.Parameters.AddWithValue("@rating", book.Rating);
                    cmd.Parameters.AddWithValue("@RatingCount", book.RatingCount);
                    cmd.Parameters.AddWithValue("@originalPrice", book.OriginalPrice);
                    cmd.Parameters.AddWithValue("@discountPrice", book.DiscountPrice);
                    cmd.Parameters.AddWithValue("@BookDetails", book.BookDetails);
                    cmd.Parameters.AddWithValue("@bookImage", book.BookImage);
                    cmd.Parameters.AddWithValue("@BookQuantity", book.BookQuantity);
                    sqlConnection.Open();
                    int result = cmd.ExecuteNonQuery();
                    //ExecuteNonQuery method is used to execute SQL Command or the storeprocedure performs, INSERT, UPDATE or Delete operations.
                    //It doesn't return any data from the database.
                    //Instead, it returns an integer specifying the number of rows inserted, updated or deleted.
                    sqlConnection.Close();
                    if (result != 0)
                    {
                        return book;
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


        public UpdateBookModel UpdateBook(UpdateBookModel updatebook)
        {
            sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
            try
            {

                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("UpdateBook", sqlConnection);//strore procedure name
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", updatebook.BookId);
                    cmd.Parameters.AddWithValue("@BookName", updatebook.BookName);
                    cmd.Parameters.AddWithValue("@authorName", updatebook.AuthorName);
                    cmd.Parameters.AddWithValue("@rating", updatebook.Rating);
                    cmd.Parameters.AddWithValue("@RatingCount", updatebook.RatingCount);
                    cmd.Parameters.AddWithValue("@originalPrice", updatebook.OriginalPrice);
                    cmd.Parameters.AddWithValue("@discountPrice", updatebook.DiscountPrice);
                    cmd.Parameters.AddWithValue("@BookDetails", updatebook.BookDetails);
                    cmd.Parameters.AddWithValue("@bookImage", updatebook.BookImage);
                    cmd.Parameters.AddWithValue("@BookQuantity", updatebook.BookQuantity);
                    sqlConnection.Open();
                    int result = cmd.ExecuteNonQuery();
                    //ExecuteNonQuery method is used to execute SQL Command or the storeprocedure performs, INSERT, UPDATE or Delete operations.
                    //It doesn't return any data from the database.
                    //Instead, it returns an integer specifying the number of rows inserted, updated or deleted.
                    sqlConnection.Close();
                    if (result != 0)
                    {
                        return updatebook;
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

        public bool DeleteBook(int BookId)
        {
            sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
            try
            {

                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("DeleteBook", sqlConnection);//strore procedure name
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId", BookId);
                    sqlConnection.Open();
                    int result = cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public BookModel GetBookByBookId(int BookId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                SqlCommand cmd = new SqlCommand("GetBookByBookId", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BookId", BookId);
                this.sqlConnection.Open();
                BookModel bookModel = new BookModel();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        bookModel.BookName = reader["BookName"].ToString();
                        bookModel.AuthorName = reader["AuthorName"].ToString();
                        bookModel.Rating = reader["Rating"].ToString();
                        bookModel.RatingCount = Convert.ToInt32(reader["RatingCount"]);
                        bookModel.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
                        bookModel.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                        bookModel.BookDetails = reader["BookDetails"].ToString();
                        bookModel.BookImage = reader["BookImage"].ToString();
                        bookModel.BookQuantity = Convert.ToInt32(reader["BookQuantity"]);
                    }

                    this.sqlConnection.Close();
                    return bookModel;
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

        public List<BookModel> GetAllBooks()
        {
            try
            {
                List<BookModel> book = new List<BookModel>();
                this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                SqlCommand cmd = new SqlCommand("GetAllBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        book.Add(new BookModel
                        {
                            BookId = Convert.ToInt32(reader["BookId"]),
                            BookName = reader["BookName"].ToString(),
                            AuthorName = reader["AuthorName"].ToString(),
                            Rating = reader["Rating"].ToString(),
                            RatingCount = Convert.ToInt32(reader["RatingCount"]),

                            OriginalPrice = Convert.ToDecimal(reader["OriginalPrice"]),
                            DiscountPrice = Convert.ToDecimal(reader["DiscountPrice"]),

                            BookDetails = reader["BookDetails"].ToString(),
                            BookImage = reader["bookImage"].ToString(),
                            BookQuantity = Convert.ToInt32(reader["BookQuantity"]),
                            
                        });
                    }

                    this.sqlConnection.Close();
                    return book;
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

    }
}