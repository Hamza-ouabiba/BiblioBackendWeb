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
    internal class LivreRespository : Repository<Livre> , ILivreRepository
    {
        public LivreRespository(DbContext _context) : base(_context)
        {
        }
        public BibliothequeDbContext bibliothequeDbContext { get => _context as BibliothequeDbContext; }
        IEnumerable ILivreRepository.LivreParAuteur()
        {
            return bibliothequeDbContext.Livres.Include(l => l.Auteur)
                .GroupBy(a => a.Auteur.NomAuteur)
                .Select(l => new {Auteur = l.Key , Livres = l.Count()})
                .ToList();
        }
    }
}
