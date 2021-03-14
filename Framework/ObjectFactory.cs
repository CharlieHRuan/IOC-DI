using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
// using IBLL;
// using IDAL;

namespace Framework
{
    public class ObjectFactory
    {
        // public static IUserDal CreateDAL()
        // {
        //     IUserDal userDal = null;
        //     //不能依赖细节，但又要创建对象
        //
        //     string config = ConfigurationManager.GetNode("IUserDAL");
        //
        //     Assembly assembly = Assembly.Load(config.Split(",")[1]);
        //     Type type = assembly.GetType(config.Split(",")[0]);
        //     userDal = (IUserDal)Activator.CreateInstance(type);
        //     return userDal;
        // }
        //
        // public static IUserBLL CreateBLL(IUserDal iUserDal)
        // {
        //     IUserBLL userBll = null;
        //
        //     string config = ConfigurationManager.GetNode("IUserBLL");
        //     Assembly assembly = Assembly.Load(config.Split(",")[1]);
        //     Type type = assembly.GetType(config.Split(",")[0]);
        //     userBll = (IUserBLL)Activator.CreateInstance(type, new object[] { iUserDal } );
        //     return userBll;
        // }
    }
}