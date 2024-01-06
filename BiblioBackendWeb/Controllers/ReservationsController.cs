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
    public class ReservationsController : ControllerBase
    {
        private readonly BibliothequeDbContext _context;

        public ReservationsController(BibliothequeDbContext context)
        {
            _context = context;
        }

        // GET: api/Reservations
        [HttpGet]
        public  IEnumerable<Reservation> GetReservations()
        {
           
            using (UnitOfWork uow = new(new BibliothequeDbContext()))
            {
                return uow.Reservation.GetAll();
            }
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public Reservation GetReservation(int id)
        {
            using (UnitOfWork uow = new(new BibliothequeDbContext()))
            {
                return uow.Reservation.Get(id);
            }
            
        }

        // PUT: api/Reservations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public  void PutReservation(int id, int idLivre, int idAdherent, DateTime dateDebut, DateTime dateFin)
        {
            using (UnitOfWork uow = new(new BibliothequeDbContext()))
            {
                Reservation reservation = uow.Reservation.Get(id);
                reservation.IdLivre = idLivre;
                reservation.IdAdherent = idAdherent;
                reservation.DateFin = dateFin;
                reservation.DateDebut = dateDebut;
             

                uow.Complete();

            }
        }

        // POST: api/Reservations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async void PostReservation(int idAdherent,int idLivre, DateTime dateDebut, DateTime dateFin)
        {
            using (UnitOfWork uow = new(new BibliothequeDbContext()))
            {
                Reservation reservation = new Reservation()
                {
                    IdAdherent = idAdherent,
                    DateDebut= dateDebut, 
                    DateFin= dateFin,
                    IdLivre= idLivre

                };
                uow.Reservation.Add(reservation);
                uow.Complete(); 

            } 
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async void DeleteReservation(int id)
        {
             
            using (UnitOfWork uow = new(new BibliothequeDbContext()))
            {
                Reservation reservation = uow.Reservation.Get(id);
                uow.Reservation.Remove(reservation);
                uow.Complete();

            }
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.IdLivre == id);
        }
    }
}
