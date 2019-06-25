namespace CJ.Domain.EntityFrameworkCore
{
    public class DefaultConnectionStringResolver: IConnectionStringResolver
    {
        public virtual string GetNameOrConnectionString()
        {
            return "Server=.\\wayne2017;Database=CoreUow;Trusted_Connection=True;";
        }
    }
}