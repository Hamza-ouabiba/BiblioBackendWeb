using System;
using System.Collections.Generic;

namespace BiblioBackendWeb.Models;

public partial class Categorie
{
    public int IdCategorie { get; set; }

    public string? NomCategorie { get; set; }

    public virtual ICollection<Livre> Livres { get; set; } = new List<Livre>();
}
