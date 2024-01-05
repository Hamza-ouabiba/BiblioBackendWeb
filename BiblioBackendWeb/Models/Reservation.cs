using System;
using System.Collections.Generic;

namespace BiblioBackendWeb.Models;

public partial class Reservation
{
    public int IdReservation { get; set; }

    public int IdLivre { get; set; }

    public int IdAdherent { get; set; }

    public DateTime? DateFin { get; set; }

    public DateTime? DateDebut { get; set; }

    public bool? Status { get; set; }

    public virtual Adherent Adherent { get; set; } = null!;

    public virtual Livre Livre { get; set; } = null!;
}
