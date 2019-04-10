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
    public class CommercialMagasinRepositoryCentrale
    {
        private readonly PersistanceDbContext context;

        public CommercialMagasinRepositoryCentrale()
        {
            context = new PersistanceDbContext();
        }

        /// <summary>
        /// Ajoute une liste de CommercialMagasin dans la base de données
        /// </summary>
        /// <param name="commercialMagasins">une liste de CommercialMagasin</param>
        public void AddRange(List<CommercialMagasin> commercialMagasins)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    context.CommercialMagasins.AddRange(commercialMagasins);

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
    }
}
