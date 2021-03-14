namespace Framework.CustomContainer
{
    public interface ICContainer
    {
        //添加一个约束，TTo一定是继承自TFrom的
        void Register<TFrom, TTo>(string shortName = null,object[] paramList = null,LifeTime lifeTime = LifeTime.Transient) where TTo : TFrom;

        TFrom Resolve<TFrom>(string shortName = null);

        public ICContainer CreateChildContainer();
    }

}