using System;
using System.Collections.Generic;

namespace BiblioBackendWeb.Models;

public partial class Etat
{
    public int IdEtat { get; set; }

    public string? Nom { get; set; }

    public virtual ICollection<Livre> Livres { get; set; } = new List<Livre>();
}
