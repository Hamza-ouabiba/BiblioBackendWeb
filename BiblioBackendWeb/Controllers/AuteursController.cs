using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BiblioBackendWeb.Models;
using BiblioBackendWeb.Repository.Implementations;
using Microsoft.AspNetCore.Authorization;

namespace BiblioBackendWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuteursController : ControllerBase
    {
        private readonly BibliothequeDbContext _context;

        public AuteursController(BibliothequeDbContext context)
        {
            _context = context;
        }

        // GET: api/Auteurs
        [HttpGet]
        public  IEnumerable<Auteur> GetAuteurs()
        {
           using(UnitOfWork uow = new(new BibliothequeDbContext()))
            {
                return uow.Auteur.GetAll();
            }
        }

        // GET: api/Auteurs/5
        [HttpGet("{id}")]
        public Auteur GetAuteur(int id)
        {
            using (UnitOfWork uow = new(new BibliothequeDbContext()))
            {
                return uow.Auteur.Get(id);
            }
        }

        // PUT: api/Auteurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuteur(int id, Auteur auteur)
        {
            if (id != auteur.IdAuteur)
            {
                return BadRequest();
            }

            _context.Entry(auteur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuteurExists(id))
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

        // POST: api/Auteurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Auteur>> PostAuteur(Auteur auteur)
        {
            using (UnitOfWork uow = new(new BibliothequeDbContext()))
            {
                uow.Auteur.Add(auteur);
                uow.Complete();
            }
            return CreatedAtAction("GetAuteur", new { id = auteur.IdAuteur }, auteur);
        }

        // DELETE: api/Auteurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuteur(int id)
        {
            var auteur = await _context.Auteurs.FindAsync(id);
            if (auteur == null)
            {
                return NotFound();
            }

            _context.Auteurs.Remove(auteur);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuteurExists(int id)
        {
            return _context.Auteurs.Any(e => e.IdAuteur == id);
        }
    }
}
