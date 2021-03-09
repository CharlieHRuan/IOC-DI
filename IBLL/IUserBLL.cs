using System;
using Model;

namespace IBLL
{
    public interface IUserBLL
    {
        UserModel Login(string account);
        void LastLogin(UserModel user);
    }
}