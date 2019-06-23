namespace CJ.Application.Test
{
    public interface IFxTest<T> where T : class
    {
        string GetFx();
    }
}