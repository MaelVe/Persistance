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
    public class UtilisateurRepository
    {
        private readonly PersistanceDbContext context;

        public UtilisateurRepository()
        {
            context = new PersistanceDbContext();
        }

        /// <summary>
        /// Cherche un utilisateur par son nom et son prénom
        /// </summary>
        /// <param name="nom">le nom de l'utilisateur</param>
        /// <param name="prenom">le prénom de l'utilisateur</param>
        /// <returns></returns>
        public Utilisateur SearchUtilisateur(string nom, string prenom)
        {
            return context.Utilisateurs.FirstOrDefault(w => w.Nom == nom && w.Prenom == prenom);
        }

        /// <summary>
        /// Récupère un utilisateur par son id
        /// </summary>
        /// <param name="idUtilisateur">l'id utilisateur</param>
        /// <returns>l'utilisateur</returns>
        public Utilisateur GetUtilisateur(int idUtilisateur)
        {
            return context.Utilisateurs.FirstOrDefault(f => f.IdUtilisateur == idUtilisateur);
        }

        /// <summary>
        /// Récupère toutes les données de la table Utilisateur
        /// </summary>
        /// <returns>Une liste de Utilisateur</returns>
        public List<Utilisateur> GetAll()
        {
            return context.Utilisateurs.ToList();
        }

    }
}
