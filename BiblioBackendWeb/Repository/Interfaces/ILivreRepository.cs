using BiblioBackendWeb.Models;
using System;
using System.Collections;

namespace BiblioBackendWeb.Repository.Interfaces
{
    public interface ILivreRepository : IRepository<Livre>
    {
         IEnumerable LivreParAuteur(); 
    }
}
