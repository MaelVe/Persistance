using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Data.Entities;

namespace Persistance.Data.Repositories
{
    public class UtilisateurRepository
    {
        private readonly PersistanceDbContext context;

        public UtilisateurRepository()
        {
            context = new PersistanceDbContext();
        }

        public Utilisateur SearchUtilisateur(string nom, string prenom)
        {
            return context.Utilisateurs.FirstOrDefault(w => w.Nom == nom && w.Prenom == prenom);
        }

        public Utilisateur GetUtilisateur(int idUtilisateur)
        {
            return context.Utilisateurs.FirstOrDefault(f => f.IdUtilisateur == idUtilisateur);
        }
        public List<Utilisateur> GetAll()
        {
            return context.Utilisateurs.ToList();
        }

    }
}
