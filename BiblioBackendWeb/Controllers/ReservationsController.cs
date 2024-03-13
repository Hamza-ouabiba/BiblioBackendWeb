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
        [HttpGet("Adherent/{idAdherent}")]
        public IEnumerable<Reservation> GetReservationByIDAdherent(int idAdherent)
        {
            using (UnitOfWork uow = new UnitOfWork(new BibliothequeDbContext()))
            {
                IEnumerable<Reservation> reservation = uow.Reservation.Find(l => l.IdAdherent== idAdherent, "Livre")
                    .Select(l => new Reservation
                    {
                        IdAdherent = l.IdLivre,
                        IdLivre = l.IdLivre, 
                        DateDebut = l.DateDebut,
                        DateFin = l.DateFin,
                        Status = l.Status,
                        Livre = new Livre {  IdLivre = l.Livre.IdLivre, Title = l.Livre.Title, IdCategorie = l.Livre.IdCategorie,IdAuteur = l.Livre.IdAuteur, Couverture = l.Livre.Couverture },
                        IdReservation = l.IdReservation,


                    });

                if (reservation == null)
                {
                    // Return a 404 Not Found response if the livre is not found
                    return [];
                }

                // Return the livre if found
                return reservation;
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
        public IActionResult PostReservation(int idAdherent, int idLivre, DateTime dateDebut, DateTime dateFin)
        {
            try
            {
                using (UnitOfWork uow = new UnitOfWork(new BibliothequeDbContext()))
                {
                     
                       Reservation reservation = new Reservation()
                        {
                            IdAdherent = idAdherent,
                            DateDebut = dateDebut,
                            DateFin = dateFin,
                            IdLivre = idLivre,
                            Status = false
                        };

                        uow.Reservation.Add(reservation);

                        if (uow.Complete() > 0)
                        { 
                            Livre livre = uow.Livre.Get(idLivre);

                            Etat etat = uow.Etat.Find(e => e.IdEtat == 2).FirstOrDefault();

                            livre.Etat = etat; 

                            uow.Complete();

                            return Ok(new { message = "Reservation added successfully", success = true });
                        }
                        else
                        {
                            return BadRequest(new { message = "Failed to add reservation", success = false });
                        }
                 
                 }
                }
            catch (Exception ex)
            { 
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}", success = false });
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
