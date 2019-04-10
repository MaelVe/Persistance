using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Data.Repositories;

namespace Persistance.Service
{
    public class ArchivageService
    {

        /// <summary>
        /// Archive la base de données à chaque synchronisation pour pouvoir retrouver les données au cas ou une synchro n'as pas bien fonctionner
        /// </summary>
        public static void Archivage()
        {
            string fileToCopy = "./Assets/bdd/PersistanceDb.db";
            if (File.Exists(fileToCopy))
            {
                if (!Directory.Exists("./Assets/Archivage"))
                {
                    Directory.CreateDirectory("./Assets/Archivage");

                }
               
                string time = DateTime.Now.ToString("ddMMMyyTHHmm");
                File.Copy(fileToCopy, Path.Combine("./Assets/Archivage/PersistanceDb-" + time + ".db"));
            }
        }

        /// <summary>
        /// Supprime toutes les données qui ont IsDeleted a true si jamais leur nombre est supérieur à 500
        /// </summary>
        public static void Purge()
        {
            var repoVisiteCentrale = new VisiteRepositoryCentrale();
            var repoVisiteLocale = new VisiteRepository();

            var deletedVisites = repoVisiteCentrale.GetDeleted();
            if(deletedVisites.Count > 500)
            {
                repoVisiteCentrale.RemoveRange(deletedVisites);
            }
        }
    }
}
