using BiblioBackendWeb.Models;
using BiblioBackendWeb.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BiblioBackendWeb.Repository.Implementations
{
    public class ReservationRepository : Repository<Reservation> , IReservationRepository
    {
        public ReservationRepository(DbContext _context) : base(_context)
        {

        }
    }
}
