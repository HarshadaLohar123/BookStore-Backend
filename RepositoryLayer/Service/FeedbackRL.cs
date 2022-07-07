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
    public class FeedbackRL:IFeedbackRL
    {
        private SqlConnection sqlConnection;
        private readonly IConfiguration configuration;
        public FeedbackRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string AddFeedback(FeedbackModel feedbackModel, int userId)
        {
            this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
            try
            {
                using(sqlConnection)
                {
                    SqlCommand command = new SqlCommand("AddFeedback", this.sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Comment",feedbackModel.Comment);
                    command.Parameters.AddWithValue("@Rating", feedbackModel.Rating);
                    command.Parameters.AddWithValue("@BookId", feedbackModel.BookId);
                    command.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    int result = command.ExecuteNonQuery();
                    sqlConnection.Close();
                    if(result > 0)
                    {
                        return "True";

                    }
                    else
                    {
                        return "Feedback Added Successfully";

                    }
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<ViewFeedbackModel> GetFeedback(int BookId)
        {
            this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("GetAllFeedback", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", BookId);
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<ViewFeedbackModel> cartmodels = new List<ViewFeedbackModel>();
                        while (reader.Read())
                        {

                            ViewFeedbackModel cartModel = new ViewFeedbackModel();
                            cartModel.BookId = Convert.ToInt32(reader["BookId"]);
                            cartModel.FullName = reader["FullName"].ToString();
                            cartModel.Comment = reader["Comment"].ToString();


                            cartModel.UserId = Convert.ToInt32(reader["UserId"]);
                            cartModel.FeedbackId = Convert.ToInt32(reader["FeedbackId"]);

                            cartModel.Rating = Convert.ToInt32(reader["Rating"]);
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
