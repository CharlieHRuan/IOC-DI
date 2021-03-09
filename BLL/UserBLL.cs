using System;
using Framework.CustomContainer;
using IBLL;
using IDAL;
using Model;

namespace BLL
{
    public class UserBLL : IUserBLL
    {
        private readonly IUserDal _userDal;

        //注入正常SqlServer
        [CPropertyInjection]
        public IUserDal UserDal { get; set; }

        //注入MySql
        [CParameterShortName("mysql")] 
        [CPropertyInjection]
        public IUserDal UserDalMySql { get; set; }

        //注入MySql
        public UserBLL([CParameterShortName("mysql")] IUserDal userDal)
        {
            _userDal = userDal;
        }

        // private readonly UserDal _userDal;
        // public UserBLL(UserDal userDal)
        // {
        //     _userDal = userDal;
        // }

        public UserModel Login(string account)
        {
            return this._userDal.Find(u => u.Account.Equals(account));
        }


        public void LastLogin(UserModel user)
        {
            user.LoginTime = DateTime.Now;
            this._userDal.Update(user);
        }
    }
}