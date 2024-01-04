using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BiblioBackendWeb.Models;

public partial class BibliothequeDbContext : DbContext
{
    public BibliothequeDbContext()
    {
    }

    public BibliothequeDbContext(DbContextOptions<BibliothequeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adherent> Adherents { get; set; }

    public virtual DbSet<Auteur> Auteurs { get; set; }

    public virtual DbSet<Categorie> Categories { get; set; }

    public virtual DbSet<Employe> Employes { get; set; }

    public virtual DbSet<Etat> Etats { get; set; }

    public virtual DbSet<Livre> Livres { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.\\;Database=BibliothequeDb;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adherent>(entity =>
        {
            entity.HasKey(e => e.IdAdherent).HasName("PK__Adherent__7FD6B6C3CA5DB3F3");

            entity.ToTable("Adherent");

            entity.Property(e => e.IdAdherent).HasColumnName("idAdherent");
            entity.Property(e => e.DateInscription).HasColumnName("dateInscription");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NomAdherent)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nomAdherent");
            entity.Property(e => e.PrenomAdherent)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("prenomAdherent");
        });

        modelBuilder.Entity<Auteur>(entity =>
        {
            entity.HasKey(e => e.IdAuteur).HasName("PK__Auteur__58A6E8B9BCE55829");

            entity.ToTable("Auteur");

            entity.Property(e => e.IdAuteur).HasColumnName("idAuteur");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Genre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NomAuteur)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nomAuteur");
        });

        modelBuilder.Entity<Categorie>(entity =>
        {
            entity.HasKey(e => e.IdCategorie).HasName("PK__Categori__8A3D2408320D83F3");

            entity.ToTable("Categorie");

            entity.Property(e => e.IdCategorie).HasColumnName("idCategorie");
            entity.Property(e => e.NomCategorie)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nomCategorie");
        });

        modelBuilder.Entity<Employe>(entity =>
        {
            entity.HasKey(e => e.IdEmploye).HasName("PK__Employe__97B32F2A5725FB37");

            entity.ToTable("Employe");

            entity.Property(e => e.IdEmploye).HasColumnName("idEmploye");
            entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");
            entity.Property(e => e.Nom)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Etat>(entity =>
        {
            entity.HasKey(e => e.IdEtat);

            entity.Property(e => e.Nom)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Livre>(entity =>
        {
            entity.HasKey(e => e.IdLivre).HasName("PK__Livre__63C546D9EE21A7A4");

            entity.ToTable("Livre");

            entity.HasIndex(e => e.IdEtat, "IX_Livre_IdEtatNavigationIdEtat");

            entity.HasIndex(e => e.IdAuteur, "IX_Livre_idAuteur");

            entity.HasIndex(e => e.IdCategorie, "IX_Livre_idCategorie");

            entity.Property(e => e.IdLivre).HasColumnName("idLivre");
            entity.Property(e => e.IdAuteur).HasColumnName("idAuteur");
            entity.Property(e => e.IdCategorie).HasColumnName("idCategorie");

            entity.HasOne(d => d.Auteur).WithMany(p => p.Livres)
                .HasForeignKey(d => d.IdAuteur)
                .HasConstraintName("FK_LIVRE_ASSOCIATI_AUTEUR");

            entity.HasOne(d => d.Categorie).WithMany(p => p.Livres)
                .HasForeignKey(d => d.IdCategorie)
                .HasConstraintName("FK_LIVRE_ASSOCIATI_CATEGORI");

            entity.HasOne(d => d.Etat).WithMany(p => p.Livres)
                .HasForeignKey(d => d.IdEtat)
                .HasConstraintName("FK_Livre_Etats_IdEtatNavigationIdEtat");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => new { e.IdLivre, e.IdAdherent }).HasName("PK__Reservat__44382DB50ED7A50C");

            entity.ToTable("Reservation");

            entity.HasIndex(e => e.IdAdherent, "IX_Reservation_idAdherent");

            entity.Property(e => e.IdLivre).HasColumnName("idLivre");
            entity.Property(e => e.IdAdherent).HasColumnName("idAdherent");
            entity.Property(e => e.DateDebut).HasColumnName("dateDebut");
            entity.Property(e => e.DateFin).HasColumnName("dateFin");

            entity.HasOne(d => d.Adherent).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.IdAdherent)
                .HasConstraintName("FK_RESERVAT_ASSOCIATI_ADHERENT");

            entity.HasOne(d => d.Livre).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.IdLivre)
                .HasConstraintName("FK_RESERVAT_ASSOCIATI_LIVRE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
