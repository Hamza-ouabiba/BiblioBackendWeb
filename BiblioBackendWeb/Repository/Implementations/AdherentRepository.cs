using BiblioBackendWeb.Models;
using BiblioBackendWeb.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioBackendWeb.Repository.Implementations
{
    public class AdherentRepository : Repository<Adherent> , IAdherentRepository
    {
        public AdherentRepository(BibliothequeDbContext bibliotheque) : base(bibliotheque) { }
        public BibliothequeDbContext bibliothequeDbContext { get => _context as BibliothequeDbContext; }

        public IEnumerable GetCreationEtat()
        {
            return bibliothequeDbContext.Etats
                .Where(e => e.Nom != "Emprunté")
                .Select(e => e)
                .ToList();
        }
        public Adherent CurrentAdherent(string Email, string password)
        {
            return bibliothequeDbContext.Adherents.FirstOrDefault(e => e.Email == Email&& e.Password == password);
        }

       


    }
}
