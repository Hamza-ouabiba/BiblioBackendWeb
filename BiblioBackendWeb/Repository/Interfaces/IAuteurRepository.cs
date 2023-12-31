using BiblioBackendWeb.Models;
using System.Collections;

namespace BiblioBackendWeb.Repository.Interfaces
{
    public interface IAuteurRepository : IRepository<Auteur>
    {
        IEnumerable TopAuthor(int count);
    }
}
