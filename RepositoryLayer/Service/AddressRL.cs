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
    public class AddressRL:IAddressRL
    {
        private SqlConnection sqlConnection;
        private readonly IConfiguration configuration;
        public AddressRL(IConfiguration configuration)
        {
            this.configuration = configuration;

        }
        private IConfiguration Configuration { get; }
        public string AddAddress(AddAddressModel addAddress, int userId)
        {
            this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);

            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("AddAddress", this.sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Address", addAddress.Address);
                    cmd.Parameters.AddWithValue("@City", addAddress.City);
                    cmd.Parameters.AddWithValue("@State", addAddress.State);
                    cmd.Parameters.AddWithValue("@TypeId", addAddress.TypeId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
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


        public string DeleteAddress(int AddressId, int UserId)
        {
            this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);

            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("DeleteAddress", this.sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AddressId", AddressId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    sqlConnection.Open();
                    int result = cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (result != 0)
                    {
                        return "Address Removed";
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

        public AddressModel UpdateAddress(AddressModel addressModel, int userId)
        {
            sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("spforUpdateAddress", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AddressId", addressModel.AddressId);
                    cmd.Parameters.AddWithValue("@Address", addressModel.Address);
                    cmd.Parameters.AddWithValue("@City", addressModel.City);
                    cmd.Parameters.AddWithValue("@State", addressModel.State);
                    cmd.Parameters.AddWithValue("@TypeId", addressModel.TypeId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    int result = cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (result != 0)
                    {
                        return addressModel;
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
