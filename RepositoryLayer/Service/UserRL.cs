using DatabaseLayer.Model;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private SqlConnection sqlConnection;
        private readonly IConfiguration configuration;
        public UserRL(IConfiguration configuration)
        {
            this.configuration = configuration;

        }
        private IConfiguration Configuration { get; }
        public UserRegModel Registration(UserRegModel userRegModel)
        {
            sqlConnection = new SqlConnection(configuration["ConnectionStrings:BookStore"]);//sql connection string
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("UserRegister", sqlConnection); //strore procedure name
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FullName", userRegModel.FullName);
                    cmd.Parameters.AddWithValue("@Email", userRegModel.Email);
                    cmd.Parameters.AddWithValue("@Password", userRegModel.Password);
                    cmd.Parameters.AddWithValue("@MobileNumber", userRegModel.MobileNumber);

                    sqlConnection.Open();
                    int result = cmd.ExecuteNonQuery();
                    //ExecuteNonQuery method is used to execute SQL Command or the storeprocedure performs, INSERT, UPDATE or Delete operations.
                    sqlConnection.Close();

                    if (result != 0)
                    {
                        return userRegModel;
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


        public string Login(UserLoginModel userLoginModel)
        {
           this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]) ;
            try
            {

                SqlCommand cmd = new SqlCommand("UserLogin", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Email", userLoginModel.Email);
                cmd.Parameters.AddWithValue("@Password", userLoginModel.Password);

                this.sqlConnection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)//HasRows:-Search there is any row or not

                {
                    int UserId = 0;
                    UserLoginModel user = new UserLoginModel();
                    while (reader.Read())//using while loop for read multiple rows.
                    {
                        user.Email = Convert.ToString(reader["Email"]);
                        user.Password = Convert.ToString(reader["Password"]);
                        UserId = Convert.ToInt32(reader["UserId"]);
                    }

                    this.sqlConnection.Close();
                    var Token = this.GenerateJWTToken(user.Email, UserId);
                    return Token;
                }
                else
                {
                    this.sqlConnection.Close();
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

        private string GenerateJWTToken(string Email, int UserId)
        {
            //generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim("Email", Email),
                    new Claim("UserId",UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(24),

                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string ForgotPassword(ForgotPasswordModel forgotPassword)
        {
            this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
            try
            {

                SqlCommand command = new SqlCommand("ForgotPassword", sqlConnection);

                command.CommandType = CommandType.StoredProcedure;


                command.Parameters.AddWithValue("@Email", forgotPassword.Email);
                this.sqlConnection.Open();

                SqlDataReader reader = command.ExecuteReader();
                //ExecuteReader method is used to execute a SQL Command or storedprocedure returns a set of rows from the database.

                if (reader.HasRows)//HasRows:-Search there is any row or not
                { 

                    while (reader.Read()) //using while loop for read multiple rows.
                    { 
                        var UserId = Convert.ToInt32(reader["UserId"]);

                        MessageQueue queue;

                        //Add message to Queue
                        if (MessageQueue.Exists(@".\private$\BookStoreQueue"))
                        {
                            queue = new MessageQueue(@".\private$\BookStoreQueue");
                        }
                        else
                        {
                            queue = MessageQueue.Create(@".\private$\BookStoreQueue");
                        }
                        Message Mymessage = new Message();
                        Mymessage.Formatter = new BinaryMessageFormatter();
                        Mymessage.Body = GenerateJWTToken(forgotPassword.Email, UserId);
                        Mymessage.Label = "Forgot Password email";
                        queue.Send(Mymessage);

                        Message msg = queue.Receive();
                        msg.Formatter = new BinaryMessageFormatter();
                        EmailService.SendMail(forgotPassword.Email, Mymessage.Body.ToString());
                        queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);
                        //queue.BeginReceive();
                        //queue.Close();
                        var token= this.GenerateJWTToken(forgotPassword.Email, UserId);

                        return token;
                    }
                }

                else
                {
                    
                    sqlConnection.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return default;
        }
      
        

        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailService.SendMail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                queue.BeginReceive();

            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode == MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied." + "Queue might be a system queue.");
                }
            }
        }

        private string GenerateToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("email", email)
                }),
                Expires = DateTime.UtcNow.AddHours(5),

                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string ResetPassword(ResetPassword resetPassword, string Email)
        {
            try
            {
                this.sqlConnection = new SqlConnection(configuration["ConnectionStrings:BookStore"]);
                {
                    if (resetPassword.Password == resetPassword.ConfirmPassword)
                    { 

                        SqlCommand cmd = new SqlCommand("ResetPassword", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.AddWithValue("@Password",resetPassword.Password);
                        sqlConnection.Open();
                        var result = cmd.ExecuteNonQuery();
                        sqlConnection.Close();
                        if (result > 0)
                        {
                            return "Congratulations! Your password has been changed successfully";
                        }
                        else
                            return "Failed to reset your password";
                    }
                    else
                        return "Make Sure your Passwords Match";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
