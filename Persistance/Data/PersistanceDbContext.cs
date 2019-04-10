using System.Data.Entity;
using Persistance.Data.Entities;

namespace Persistance.Data
{ 
    /// <summary>
    /// Contexte de la base de données locale
    /// </summary>
    public class PersistanceDbContext : DbContext
    {        
        /// <summary>
        /// Constructeur ciblant la base de données locale en sqlite
        /// </summary>
        public PersistanceDbContext()
            : base("name=PersistanceDb")
        {
            Database.SetInitializer<PersistanceDbContext>(null);
            this.Configuration.LazyLoadingEnabled = false;
        }

        // Ajout d'un DbSet pour chaque type d'entité inclue dans le modèle.
        public DbSet<Magasin> Magasins { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Visite> Visites { get; set; }
        public DbSet<CommercialMagasin> CommercialMagasins { get; set; }
    }
}