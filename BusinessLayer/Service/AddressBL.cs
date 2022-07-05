using BusinessLayer.Interface;
using DatabaseLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class AddressBL:IAddressBL
    {
        private readonly IAddressRL addressRL;

        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }

        public string AddAddress(AddAddressModel addAddress, int userId)
        {
            try
            {
                return this.addressRL.AddAddress(addAddress, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DeleteAddress(int AddressId, int UserId)
        {
            try
            {
                return this.addressRL.DeleteAddress(AddressId, UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public AddressModel UpdateAddress(AddressModel addressModel, int userId)
        {
            try
            {
                return this.addressRL.UpdateAddress(addressModel, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
