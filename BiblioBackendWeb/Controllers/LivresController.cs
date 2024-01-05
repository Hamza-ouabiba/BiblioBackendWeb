using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BiblioBackendWeb.Models;
using BiblioBackendWeb.Repository.Implementations;
using BiblioBackendWeb.Utils;
using Microsoft.AspNetCore.Components.QuickGrid;

namespace BiblioBackendWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivresController : ControllerBase
    {
        private readonly BibliothequeDbContext _context;

        public LivresController(BibliothequeDbContext context)
        {
            _context = context;
        }

        // GET: api/Livres
        [HttpGet]
        public IEnumerable<Livre> GetLivres()
        {
            using (UnitOfWork uow = new UnitOfWork(new BibliothequeDbContext()))
            {
                return uow.Livre.Find(includeProperties: "Auteur,Categorie,Etat")
                    .Select(l => new Livre
                    {
                        IdLivre = l.IdLivre,
                        Title = l.Title,
                        Description = l.Description,
                        Prix = l.Prix,
                        DatePublication = l.DatePublication,
                         Categorie = new Categorie { NomCategorie = l.Categorie.NomCategorie },
                        Etat = new Etat { Nom = l.Etat.Nom },
                        Auteur = new Auteur { NomAuteur = l.Auteur.NomAuteur },
                        NbPages = l.NbPages,
                     }).ToList();
            }
        }

        // GET: api/Livres/5
        [HttpGet("{id}")]
        public Livre GetLivre(int id)
        {
            using (UnitOfWork uow = new UnitOfWork(new BibliothequeDbContext()))
            {
                Livre livre = uow.Livre.Find(l => l.IdLivre == id, "Auteur,Categorie,Etat")
                    .FirstOrDefault();

                if (livre != null)
                {
                    // Project the related entities if the livre is found
                    livre.Categorie = new Categorie { NomCategorie = livre.Categorie.NomCategorie };
                    livre.Etat = new Etat { Nom = livre.Etat.Nom };
                    livre.Auteur = new Auteur { NomAuteur = livre.Auteur.NomAuteur };
                }
                else
                {
                    livre =null;
                }

                return livre;
            }
        }



        [HttpGet("title/{title}")]
        public IEnumerable<Livre> GetLivreByTitle(string title)
        {
            using (UnitOfWork uow = new UnitOfWork(new BibliothequeDbContext()))
            {
                IEnumerable < Livre > livres= uow.Livre.Find(l => l.Title.StartsWith(title), "Auteur,Categorie,Etat")
                    .Select(l => new Livre
                    {
                        IdLivre = l.IdLivre,
                        Title = l.Title,
                        Description = l.Description,
                        Prix = l.Prix,
                        DatePublication = l.DatePublication,
                        Categorie = new Categorie { NomCategorie = l.Categorie.NomCategorie },
                        Etat = new Etat { Nom = l.Etat.Nom },
                        Auteur = new Auteur { NomAuteur = l.Auteur.NomAuteur },
                        NbPages = l.NbPages,
                    }) ;

                if (livres == null)
                {
                    // Return a 404 Not Found response if the livre is not found
                    return [];
                }

                // Return the livre if found
                return livres;
            }
        }
        [HttpGet("Categorie/{idCategory}")]
        public IEnumerable<Livre> GetLivreByCategory(int idCategory)
        {
            using (UnitOfWork uow = new UnitOfWork(new BibliothequeDbContext()))
            {
                IEnumerable<Livre> livres = uow.Livre.Find(l => l.IdCategorie == idCategory, "Auteur,Categorie,Etat")
                    .Select(l => new Livre
                    {
                        IdLivre = l.IdLivre,
                        Title = l.Title,
                        Description = l.Description,
                        Prix = l.Prix,
                        DatePublication = l.DatePublication,
                        Categorie = new Categorie { NomCategorie = l.Categorie.NomCategorie },
                        Etat = new Etat { Nom = l.Etat.Nom },
                        Auteur = new Auteur { NomAuteur = l.Auteur.NomAuteur },
                        NbPages = l.NbPages,
                    }) ;

                if (livres == null)
                {
                    // Return a 404 Not Found response if the livre is not found
                    return [];
                }

                // Return the livre if found
                return livres;
            }
        }
        // PUT: api/Livres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivre(int id, Livre livre)
        {
            if (id != livre.IdLivre)
            {
                return BadRequest();
            }

            _context.Entry(livre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Livres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Livre>> PostLivre(Livre livre)
        {
            _context.Livres.Add(livre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLivre", new { id = livre.IdLivre }, livre);
        }

        // DELETE: api/Livres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivre(int id)
        {
            var livre = await _context.Livres.FindAsync(id);
            if (livre == null)
            {
                return NotFound();
            }

            _context.Livres.Remove(livre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LivreExists(int id)
        {
            return _context.Livres.Any(e => e.IdLivre == id);
        }
    }
}
