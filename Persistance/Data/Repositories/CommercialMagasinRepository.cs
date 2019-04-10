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
    public class CommercialMagasinRepository
    {
        private readonly PersistanceDbContext context;

        public CommercialMagasinRepository()
        {
            context = new PersistanceDbContext();
        }

        /// <summary>
        /// Récupère toutes les données de la table CommercialMagasin
        /// </summary>
        /// <returns>Une liste de CommercialMagasin</returns>
        public List<CommercialMagasin> GetAll()
        {
            return context.CommercialMagasins.ToList();
        }
    }
}
