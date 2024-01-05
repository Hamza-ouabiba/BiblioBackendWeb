using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BiblioBackendWeb.Models;
using BiblioBackendWeb.Repository.Implementations;

namespace BiblioBackendWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdherentsController : ControllerBase
    {
        private readonly BibliothequeDbContext _context;

        public AdherentsController(BibliothequeDbContext context)
        {
            _context = context;
        }

        // GET: api/Adherents
        [HttpGet]
        public  IEnumerable<Adherent> GetAdherents()
        {
            using (UnitOfWork uow = new(new BibliothequeDbContext()))
            {
                return uow.Adherent.GetAll();
            }
        }

        // GET: api/Adherents/5
        [HttpGet("{id}")]
        public  Adherent GetAdherent(int id)
        {
             using (UnitOfWork uow = new(new BibliothequeDbContext()))
            {
                return uow.Adherent.Get(id);
            }
             
        }

        // PUT: api/Adherents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public void PutAdherent(int id, string nomAdherent, string prenomAdherent, string Email)
        {
            using (UnitOfWork uow = new(new BibliothequeDbContext()))
            {
                Adherent adherent = uow.Adherent.Get(id);
                adherent.Email = Email;
                adherent.PrenomAdherent = prenomAdherent;
                adherent.NomAdherent = nomAdherent;
              

                uow.Complete();

            }
        }

        // POST: api/Adherents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public  void PostAdherent(string nomAdherent,string prenomAdherent,string Email)
        {
            Adherent adherent = new Adherent()
            {
                NomAdherent = nomAdherent,
                PrenomAdherent = prenomAdherent,
                Email = Email,
                DateInscription = DateTime.Now
        };
           

            using (UnitOfWork uow = new(new BibliothequeDbContext()))
            {
                 
                 uow.Adherent.Add(adherent);
                 uow.Complete();
            } 


        }
        // DELETE: api/Adherents/5
        [HttpDelete("{id}")]
        public void DeleteAdherent(int id)
        {
            using (UnitOfWork uow = new(new BibliothequeDbContext()))
            {
                Adherent adherent =  uow.Adherent.Get(id);
                uow.Adherent.Remove(adherent);
                uow.Complete();

            }
        }

        private bool AdherentExists(int id)
        {
            return _context.Adherents.Any(e => e.IdAdherent == id);
        }
    }
}
