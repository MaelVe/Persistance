using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Data.Entities;

namespace Persistance.Data.Repositories
{
    public class MagasinRepositoryCentrale
    {
        private readonly PersistanceDbContextCentrale context;

        public MagasinRepositoryCentrale()
        {
            context = new PersistanceDbContextCentrale();
        }

        public void AddRange(List<Magasin> visites)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    context.Magasins.AddRange(visites);

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

        public Magasin GetMagasin(int idMagasin)
        {
            return context.Magasins.FirstOrDefault(f => f.IdMagasin == idMagasin);
        }

        public Magasin GetMagasinByNom(string nomMagasin)
        {
            return context.Magasins.FirstOrDefault(f => f.NomMagasin == nomMagasin);
        }

        public List<Magasin> GetAll()
        {
            return context.Magasins.ToList();
        }
    }
}
