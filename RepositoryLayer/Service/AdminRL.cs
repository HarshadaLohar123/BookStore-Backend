using DatabaseLayer.Model;
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
    public class AdminRL:IAdminRL
    {
        private SqlConnection sqlConnection;
        public AdminRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }


        public AdminLoginModel Adminlogin(AdminResponse adminResponse)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                SqlCommand cmd = new SqlCommand("LoginAdmin", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Email", adminResponse.Email);
                cmd.Parameters.AddWithValue("@Password", adminResponse.Password);
                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AdminLoginModel admin = new AdminLoginModel();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        admin.AdminId = Convert.ToInt32(reader["AdminId"]);
                        admin.FullName = Convert.ToString(reader["FullName"]);
                        admin.Email = Convert.ToString(reader["Email"]);
                        admin.MobileNumber = Convert.ToString(reader["MobileNumber"]);
                    }

                    this.sqlConnection.Close();
                    admin.Token = this.GetJWTToken(admin);
                    return admin;
                }
                else
                {
                    throw new Exception("Email Or Password Is Wrong");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetJWTToken(AdminLoginModel admin)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim("Email", admin.Email),
                    new Claim("AdminId",admin.AdminId.ToString())
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
    }
}
