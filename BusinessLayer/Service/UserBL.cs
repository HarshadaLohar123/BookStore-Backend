using BusinessLayer.Interface;
using DatabaseLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL:IUserBL
    {
        private readonly IUserRL userRL;

        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }


        public UserRegModel Registration(UserRegModel userRegModel)
        {
            try
            {
                return this.userRL.Registration(userRegModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Login(UserLoginModel userLoginModel)
        {
            try
            {
                return this.userRL.Login(userLoginModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ForgotPassword(ForgotPasswordModel forgotPassword)
        {
            try
            {
                return this.userRL.ForgotPassword(forgotPassword);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string ResetPassword(ResetPassword resetPassword, string Email)
        {
            try
            {
                return userRL.ResetPassword(resetPassword, Email);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
