using BiblioBackendWeb.Models;
using BiblioBackendWeb.Repository.Interfaces;

namespace BiblioBackendWeb.Repository.Implementations
{
    public class EmployeRepository : Repository<Employe>, IEmployeRepository
    {
        public EmployeRepository(BibliothequeDbContext context)  : base(context) { }
        public IEnumerable<Employe> TopEmployees(int count)
        {
            throw new NotImplementedException();
        }
    }
}
