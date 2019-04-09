using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Data.Entities;

namespace Persistance.Data.Repositories
{
    public class CommercialMagasinRepository
    {
        private readonly PersistanceDbContext context;

        public CommercialMagasinRepository()
        {
            context = new PersistanceDbContext();
        }

        public List<CommercialMagasin> GetAll()
        {
            return context.CommercialMagasins.ToList();
        }
    }
}
