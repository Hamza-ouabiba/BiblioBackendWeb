using System;
using System.Collections.Generic;

namespace BiblioBackendWeb.Models;

public partial class Adherent
{
    public int IdAdherent { get; set; }

    public string? NomAdherent { get; set; }

    public string? PrenomAdherent { get; set; }

    public DateTime? DateInscription { get; set; }

    public string? Email { get; set; }
    public string? Password { get; set; }


    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
