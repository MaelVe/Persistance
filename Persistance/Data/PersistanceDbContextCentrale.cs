using System.Data.Entity;
using Npgsql;
using Persistance.Data.Entities;

namespace Persistance.Data
{
    /// <summary>
    /// Contexte de la base de données centrale
    /// </summary>
    public class PersistanceDbContextCentrale : DbContext
    {
        /// <summary>
        /// Constructeur ciblant la base de données centrale en postgresql
        /// </summary>
        public PersistanceDbContextCentrale()
           : base(nameOrConnectionString: "PersistanceNpgsql")
        {
            Database.SetInitializer<PersistanceDbContextCentrale>(null);
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }

        // Ajout d'un DbSet pour chaque type d'entité inclue dans le modèle.
        public DbSet<Magasin> Magasins { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Visite> Visites { get; set; }
        public DbSet<CommercialMagasin> CommercialMagasins { get; set; }
    }
}