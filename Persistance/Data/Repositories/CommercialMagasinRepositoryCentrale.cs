using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Data.Entities;

namespace Persistance.Data.Repositories
{
    public class CommercialMagasinRepositoryCentrale
    {
        private readonly PersistanceDbContext context;

        public CommercialMagasinRepositoryCentrale()
        {
            context = new PersistanceDbContext();
        }

        public void AddRange(List<CommercialMagasin> visites)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    context.CommercialMagasins.AddRange(visites);

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
