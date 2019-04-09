using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Service
{
    public class ArchivageService
    {
        public ArchivageService()
        {

        }

        public static void Archivage()
        {
            // File.Copy(Path.Combine("./Assets/bdd/PersistanceDb.db"), Path.Combine("./Assets/Archivage/PersistanceDb-"+DateTime.Now.ToString()+".db.old"));
        }
    }
}
