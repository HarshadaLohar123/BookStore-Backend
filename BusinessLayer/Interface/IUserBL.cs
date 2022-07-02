using DatabaseLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public UserRegModel Registration(UserRegModel userRegModel);
        public string Login(UserLoginModel userLoginModel);
        public string ForgotPassword(ForgotPasswordModel forgotPassword);

        string ResetPassword(ResetPassword resetPassword, string Email);

    }
}
