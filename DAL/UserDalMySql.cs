using System;
using System.Linq.Expressions;
using IDAL;
using Model;

namespace DAL
{
    public class UserDalMySql : IUserDal
    {
        public UserModel Find(Expression<Func<UserModel, bool>> expression)
        {
            return new UserModel()
            {
                ID = 7,
                Name = "Ruanheng-MySql",
                Account = "Administrator",
                Email = "ruanheng1995@gmail.com",
                Password = "123123",
                Role = "Admin",
                LoginTime = DateTime.Now
            };
        }

        public void Update(UserModel userModel)
        {
            Console.WriteLine("数据库已更新");
        }
    }
}