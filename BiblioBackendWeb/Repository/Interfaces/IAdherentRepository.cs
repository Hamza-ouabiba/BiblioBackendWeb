using BiblioBackendWeb.Models;
using BiblioBackendWeb.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioBackendWeb.Repository.Interfaces
{
    public interface IAdherentRepository : IRepository<Adherent>
    {
        Adherent CurrentAdherent(string username, string password);
    }
}
