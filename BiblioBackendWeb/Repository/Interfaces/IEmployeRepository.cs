using BiblioBackendWeb.Models;

namespace BiblioBackendWeb.Repository.Interfaces
{
    public interface IEmployeRepository : IRepository<Employe>
    {
        IEnumerable<Employe> TopEmployees(int count);
    }
}
