namespace CJ.Application.Test
{
    public class FxTest<T,TT> : IFxTest<T> where  T:class 
    {
        public string GetFx()
        {
            return typeof(T).FullName;
        }
    }
}