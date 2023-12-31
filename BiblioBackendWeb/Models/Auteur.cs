using System;
using System.Collections.Generic;

namespace BiblioBackendWeb.Models;

public partial class Auteur
{
    public int IdAuteur { get; set; }

    public string? NomAuteur { get; set; }

    public string? Email { get; set; }

    public string? Genre { get; set; }

    public virtual ICollection<Livre> Livres { get; set; } = new List<Livre>();
}
