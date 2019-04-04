using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Data.Entities;

namespace Persistance.Data.Repositories
{
    public class VisiteRepository
    {
        private readonly PersistanceDbContext context;

        public VisiteRepository()
        {
            context = new PersistanceDbContext();
        }

        public void Add()
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Console.Write("Enter a name for a new Blog: ");
                    var name = Console.ReadLine();

                    var blog = new Magasin { Enseigne = "leclerc", NomMagasin = "qcecef" };
                    context.Magasins.Add(blog);

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

        public Visite GetVisite(int idVisite)
        {
            return context.Visites.FirstOrDefault(f => f.IdVisite == idVisite);
        }

        public List<Visite> GetVisiteByMagasin(int idMagasin)
        {
            return context.Visites.Where(w => w.IdMagasin == idMagasin).ToList();
        }
    }
}
