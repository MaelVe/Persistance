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
    public class UtilisateurRepositoryCentrale
    {
        private readonly PersistanceDbContextCentrale context;

        public UtilisateurRepositoryCentrale()
        {
            context = new PersistanceDbContextCentrale();
        }

        /// <summary>
        /// Ajoute une liste de Utilisateur dans la base de données centrale
        /// </summary>
        /// <param name="commercialMagasins">une liste de Utilisateur</param>
        public void AddRange(List<Utilisateur> utilisateurs)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Utilisateurs.AddRange(utilisateurs);

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
