using BiblioBackendWeb.Models;
using BiblioBackendWeb.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioBackendWeb.Repository.Implementations
{
    public class AdherentRepository : Repository<Adherent> , IAdherentRepository
    {
        public AdherentRepository(DbContext _context) : base(_context)
        {

        }

    }
}
