using System;
using System.Collections.Generic;

namespace BiblioBackendWeb.Models;

public partial class Livre
{
    public int IdLivre { get; set; }

    public int IdCategorie { get; set; }

    public int IdAuteur { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public byte[]? Couverture { get; set; }

    public float Prix { get; set; }

    public int NbPages { get; set; }

    public DateTime DatePublication { get; set; }

    public int? IdEtat { get; set; }

    public virtual Auteur Auteur { get; set; } = null!;

    public virtual Categorie Categorie { get; set; } = null!;

    public virtual Etat? Etat { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
