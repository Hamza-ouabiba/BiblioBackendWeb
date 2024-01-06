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
    [AllowAnonymous]
    [Route("api/[controller]")] 
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly BibliothequeDbContext _context;

        public CategoriesController(BibliothequeDbContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
         [AllowAnonymous]
        public  IEnumerable<Categorie> GetCategories()
        {
            using (UnitOfWork uow = new(new BibliothequeDbContext()))
            {
                return uow.Category.GetAll();
            }
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public Categorie GetCategorie(int id)
        {
            using (UnitOfWork uow = new(new BibliothequeDbContext()))
            {
                return uow.Category.Get(id);
            }
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategorie(int id, Categorie categorie)
        {
            if (id != categorie.IdCategorie)
            {
                return BadRequest();
            }

            _context.Entry(categorie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategorieExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Categorie>> PostCategorie(Categorie categorie)
        {
            _context.Categories.Add(categorie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategorie", new { id = categorie.IdCategorie }, categorie);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategorie(int id)
        {
            var categorie = await _context.Categories.FindAsync(id);
            if (categorie == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categorie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategorieExists(int id)
        {
            return _context.Categories.Any(e => e.IdCategorie == id);
        }
    }
}
