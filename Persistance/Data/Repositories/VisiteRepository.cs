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

        public void Add(Visite visite)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    
                    context.Visites.Add(visite);

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

        public void Update(Visite visite)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var entity = context.Visites.Find(visite.IdVisite);
                    if (entity == null)
                    {
                        return;
                    }

                    context.Entry(entity).CurrentValues.SetValues(visite);
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

        public void Remove(Visite visite)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var entity = context.Visites.Remove(visite);
                   
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
