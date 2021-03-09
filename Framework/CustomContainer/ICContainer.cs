namespace Framework.CustomContainer
{
    public interface ICContainer
    {
        //添加一个约束，TTo一定是继承自TFrom的
        void Register<TFrom, TTo>(string shortName = null,object[] paramList = null) where TTo : TFrom;

        TFrom Resolve<TFrom>(string shortName = null);
    }
}