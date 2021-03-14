using System;
using System.Text.Json.Serialization;
using BLL;
using DAL;
using Framework;
using Framework.CustomAOP;
using Framework.CustomContainer;
using IBLL;
using IDAL;
using IOCDI.Interface;
using IOCDI.Service;

namespace ConsoleProject
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                #region MyRegion

                Console.WriteLine("Hello World!");
                // {
                //     //不应该这么写，没有遵循依赖倒置原则（建议）
                //     UserDAL userDal = new UserDAL();
                //     UserBLL userBll = new UserBLL(userDal);
                //
                // }

                // 解释IOC
                // {
                //     //使用SQLServer时
                //     // IUserDal iUserDal = new UserDal(); 
                //     //使用MySql时
                //     IUserDal iUserDal = new UserDalMySql();
                //     IUserBLL iUserBll = new UserBLL(iUserDal);
                // }

                //贯彻DIP
                //{
                //    //抽象不能实例化，怎么办
                //    //但是我又不想要细节
                //    //引入第三方（IOC就是第三方）
                //    // IUserDal iUserDal = new IUserDal();
                //    // IUserBLL iUserBll = new IUserBLL();

                //    IUserDal iUserDal = ObjectFactory.CreateDAL();
                //    IUserBLL iUserBll = ObjectFactory.CreateBLL(iUserDal);


                //    var user = iUserBll.Login("Administrator");

                //    Console.WriteLine(user.Name);
                //} 

                #endregion

                #region IOC容器

                // 常规IOC容器：容器对象——注册——生成
                // ICContainer iCContainer = new CContainer();
                // iCContainer.Register<IUserDal, UserDal>();
                // iCContainer.Register<IUserDal, UserDalMySql>("mysql");
                // iCContainer.Register<IUserBLL, UserBLL>();
                // iCContainer.Register<ITestServiceA, TestServiceA>(paramList: new object[] { "Jack", 3 });
                // iCContainer.Register<ITestServiceB, TestServiceB>();
                // iCContainer.Register<ITestServiceC, TestServiceC>();
                // iCContainer.Register<ITestServiceD, TestServiceD>();
                // iCContainer.Register<ITestServiceE, TestServiceE>();

                // ITestServiceD iTestServiceD = iCContainer.Resolve<ITestServiceD>();
                // ITestServiceE iTestServiceE = iCContainer.Resolve<ITestServiceE>();

                // IUserBLL iUserBll = iCContainer.Resolve<IUserBLL>();
                // IUserDal iUserDalSqlServer = iCContainer.Resolve<IUserDal>();
                // IUserDal iUserDalMySql = iCContainer.Resolve<IUserDal>("MySql");
                // Console.WriteLine(iUserDalSqlServer.Find(x => x.Account == "").Name);
                // Console.WriteLine(iUserDalMySql.Find(x => x.Account == "").Name);
                //
                // ITestServiceA iTestServiceA = iCContainer.Resolve<ITestServiceA>();

                // IUserBLL iUserBll = iCContainer.Resolve<IUserBLL>();

                #endregion

                #region 生命周期管理

                // ICContainer container = new CContainer();
                // container.Register<ITestServiceB, TestServiceB>(lifeTime:LifeTime.Scoped);
                //
                // ITestServiceB b1 = container.Resolve<ITestServiceB>();
                // ITestServiceB b2 = container.Resolve<ITestServiceB>();

                // Console.WriteLine(object.ReferenceEquals(b1, b2));  //False 因为每次都是重新创建
                // Console.WriteLine(object.ReferenceEquals(b1, b2));  //Ture 因为第一次创建了，第二次是直接引用
                //
                // ICContainer container1 = container.CreateChildContainer();
                // ITestServiceB b11 = container1.Resolve<ITestServiceB>();
                // ITestServiceB b12 = container1.Resolve<ITestServiceB>();
                //
                // ICContainer container2 = container.CreateChildContainer();
                // ITestServiceB b21 = container2.Resolve<ITestServiceB>();
                // ITestServiceB b22 = container2.Resolve<ITestServiceB>();
                //
                //
                // Console.WriteLine(object.ReferenceEquals(b11,b12)); //T
                // Console.WriteLine(object.ReferenceEquals(b21,b22)); //T
                //
                // Console.WriteLine(object.ReferenceEquals(b11,b22)); //F
                // Console.WriteLine(object.ReferenceEquals(b12,b22)); //F

                #endregion

                #region AOP扩展

                // CustomAOPTest.Show();

                ICContainer iContainer = new CContainer();
                iContainer.Register<ITestServiceB, TestServiceB>();
                ITestServiceB testService = iContainer.Resolve<ITestServiceB>();
                testService.Show();
                Console.WriteLine($"***********************");
                // testService = (ITestServiceB) testService.AOP(typeof(ITestServiceB));
                // testService.Show();
                // testService.Show_1();

                #endregion
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}