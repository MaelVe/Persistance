using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Data.Entities;

namespace Persistance.Data.Repositories
{
    public class UtilisateurRepositoryCentrale
    {
        private readonly PersistanceDbContextCentrale context;

        public UtilisateurRepositoryCentrale()
        {
            context = new PersistanceDbContextCentrale();
        }

        public void AddRange(List<Utilisateur> visites)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Utilisateurs.AddRange(visites);

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
        public Utilisateur SearchUtilisateur(string nom, string prenom)
        {
            return context.Utilisateurs.FirstOrDefault(w => w.Nom == nom && w.Prenom == prenom);
        }

        public Utilisateur GetUtilisateur(int idUtilisateur)
        {
            return context.Utilisateurs.FirstOrDefault(f => f.IdUtilisateur == idUtilisateur);
        }
    }
}
