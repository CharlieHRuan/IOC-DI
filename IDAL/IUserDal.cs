using System;
using System.Linq.Expressions;
using Model;

namespace IDAL
{
    public interface IUserDal
    {
        UserModel Find(Expression<Func<UserModel, bool>> expression);

        void Update(UserModel userModel);
    }
}
