using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Data.Entities;

namespace Persistance.Data.Repositories
{
    class MagasinRepository
    {
        private readonly PersistanceDbContext context;

        public MagasinRepository()
        {
            context = new PersistanceDbContext();
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
            //return context.Magasins.Where()
        }

        public Magasin GetMagasin(int idMagasin)
        {
            return context.Magasins.FirstOrDefault(f => f.IdMagasin == idMagasin);
        }

        public Magasin GetMagasinByNom(string nomMagasin)
        {
            return context.Magasins.FirstOrDefault(f => f.NomMagasin == nomMagasin);
        }

    }
}
