using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Data.Entities;

namespace Persistance.Data.Repositories
{
    /// <summary>
    /// Classe regroupant les méthodes CRUD sur une table de la base de données
    /// </summary>
    public class MagasinRepositoryCentrale
    {
        private readonly PersistanceDbContextCentrale context;

        public MagasinRepositoryCentrale()
        {
            context = new PersistanceDbContextCentrale();
        }

        /// <summary>
        /// Ajoute une liste de Magasin dans la base de données centrale
        /// </summary>
        /// <param name="commercialMagasins">une liste de Magasin</param>
        public void AddRange(List<Magasin> magasins)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    context.Magasins.AddRange(magasins);

                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
       
        /// <summary>
        /// Récupère toutes les données de la table Magasin
        /// </summary>
        /// <returns>Une liste de Magasin</returns>
        public List<Magasin> GetAll()
        {
            return context.Magasins.ToList();
        }
    }
}
