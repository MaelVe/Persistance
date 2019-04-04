﻿using System.Data.Entity;

namespace Persistance.Data.Entities { 

    public class PersistanceDbContext : DbContext
    {
        // Votre contexte a été configuré pour utiliser une chaîne de connexion « PersistanceModel » du fichier 
        // de configuration de votre application (App.config ou Web.config). Par défaut, cette chaîne de connexion cible 
        // la base de données « Persistance.Repository.PersistanceModel » sur votre instance LocalDb. 
        // 
        // Pour cibler une autre base de données et/ou un autre fournisseur de base de données, modifiez 
        // la chaîne de connexion « PersistanceModel » dans le fichier de configuration de l'application.
        public PersistanceDbContext()
            : base("name=PersistanceDb")
        {
            Database.SetInitializer<PersistanceDbContext>(null);
            this.Configuration.LazyLoadingEnabled = false;
        }

        // Ajoutez un DbSet pour chaque type d'entité à inclure dans votre modèle. Pour plus d'informations 
        // sur la configuration et l'utilisation du modèle Code First, consultez http://go.microsoft.com/fwlink/?LinkId=390109.
        public DbSet<Magasin> Magasins { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Visite> Visites { get; set; }
        public DbSet<CommercialMagasin> CommercialMagasins { get; set; }
    }
}