using DatabaseLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UserRegModel Registration(UserRegModel userRegModel);
        public string Login(UserLoginModel userLoginModel);

        string ForgotPassword(ForgotPasswordModel forgotPassword);

        string ResetPassword(ResetPassword resetPassword, string Email);
    }
}
