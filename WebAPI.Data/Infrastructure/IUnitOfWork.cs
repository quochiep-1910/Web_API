namespace WebAPI.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}