using System;
using System.Collections.Generic;

namespace BiblioBackendWeb.Models;

public partial class Reservation
{
    public int IdLivre { get; set; }

    public int IdAdherent { get; set; }

    public DateOnly? DateFin { get; set; }

    public DateOnly? DateDebut { get; set; }

    public virtual Adherent Adherent { get; set; } = null!;

    public virtual Livre Livre { get; set; } = null!;
}
