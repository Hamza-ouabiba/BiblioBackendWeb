
namespace BiblioBackendWeb.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
     
        int Complete();
    }
}
