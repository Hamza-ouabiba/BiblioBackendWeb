
using BiblioBackendWeb.Models;
using BiblioBackendWeb.Repository.Interfaces;

namespace BiblioBackendWeb.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BibliothequeDbContext _context;
         public IAuteurRepository Auteur { get; private set; }
        public ILivreRepository Livre { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IEtatRepository Etat { get; private set; }
        public IAdherentRepository Adherent { get; private set; }
        public IReservationRepository Reservation { get; private set; }
        public UnitOfWork(BibliothequeDbContext context)
        {
            _context = context;
            Auteur = new AuteurRepository(context);
             Category = new CategoryRepository(context);
            Livre  = new LivreRespository(context);
            Etat = new EtatRepository(context);
            Adherent = new AdherentRepository(context);
            Reservation = new ReservationRepository(context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
