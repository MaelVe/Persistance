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
    public class MagasinRepository
    {
        private readonly PersistanceDbContext context;

        public MagasinRepository()
        {
            context = new PersistanceDbContext();
        }

        /// <summary>
        /// Récupère les magasins en fonction d'un utilisateur
        /// </summary>
        /// <param name="utilisateur">L'utilisateur actuel</param>
        /// <returns>Une liste de magasins</returns>
        public  List<Magasin> GetMagasinByUtilisateur(Utilisateur utilisateur)
        {
            List<Magasin> magasins = new List<Magasin>();
            var allRelations = context.CommercialMagasins.Where(w => w.IdUtilisateur == utilisateur.IdUtilisateur);
            foreach (var oneRelation in allRelations)
            {
                magasins.Add(context.Magasins.FirstOrDefault(f => f.IdMagasin == oneRelation.IdMagasin));
            }

            return magasins;
        }

        /// <summary>
        /// Récupère un magasin  grâce à son id
        /// </summary>
        /// <param name="idMagasin">l'id du magasin</param>
        /// <returns>un magasin</returns>
        public Magasin GetMagasin(int idMagasin)
        {
            return context.Magasins.FirstOrDefault(f => f.IdMagasin == idMagasin);
        }

        /// <summary>
        /// Récupère un magasin  grâce à son nom
        /// </summary>
        /// <param name="nomMagasin"></param>
        /// <returns>un magasin</returns>
        public Magasin GetMagasinByNom(string nomMagasin)
        {
            return context.Magasins.FirstOrDefault(f => f.NomMagasin == nomMagasin);
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
