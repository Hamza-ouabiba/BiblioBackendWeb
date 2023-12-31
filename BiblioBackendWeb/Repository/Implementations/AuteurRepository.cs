using BiblioBackendWeb.Models;
using BiblioBackendWeb.Repository.Implementations;
using System;
using System.Collections;

namespace BiblioBackendWeb.Repository.Interfaces
{
    public class AuteurRepository : Repository<Auteur>, IAuteurRepository
    {
        public AuteurRepository(BibliothequeDbContext context):base(context)
        {

        }
        public IEnumerable TopAuthor(int count)
        {
            throw new NotImplementedException();
        }
    }
}
