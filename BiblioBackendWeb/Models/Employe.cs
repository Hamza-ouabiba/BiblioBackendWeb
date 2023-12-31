using System;
using System.Collections.Generic;

namespace BiblioBackendWeb.Models;

public partial class Employe
{
    public int IdEmploye { get; set; }

    public string? Email { get; set; }

    public string? Genre { get; set; }

    public bool? IsAdmin { get; set; }

    public string? Nom { get; set; }
}
