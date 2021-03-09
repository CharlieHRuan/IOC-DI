using System;
using System.Linq.Expressions;
using Framework.CustomContainer;
using IDAL;
using IOCDI.Interface;
using Model;

namespace DAL
{
    public class UserDal : IUserDal
    {
        private readonly ITestServiceC _iTestServiceC;
        private readonly ITestServiceD _iTestServiceD;
        private readonly ITestServiceE _iTestServiceE;
        private readonly ITestServiceA _iTestServiceA;
        private readonly ITestServiceB _iTestServiceB;

        // [CConstructor]   //为了测试属性注入，可以暂时把这个注释掉
        public UserDal(ITestServiceA iTestServiceA,ITestServiceB iTestServiceB)
        {
            _iTestServiceA = iTestServiceA;
            _iTestServiceB = iTestServiceB;
        }

        public UserDal(ITestServiceC iTestServiceC, ITestServiceD iTestServiceD, ITestServiceE iTestServiceE)
        {
            _iTestServiceC = iTestServiceC;
            _iTestServiceD = iTestServiceD;
            _iTestServiceE = iTestServiceE;
        }


        public UserModel Find(Expression<Func<UserModel, bool>> expression)
        {
            return new UserModel()
            {
                ID = 7,
                Name = "Ruanheng-SQLServer",
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