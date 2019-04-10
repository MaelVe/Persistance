using System;
using Persistance.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using Persistance.Data;
using Persistance.Data.Repositories;
using Persistance.Data.Entities;

namespace Persistance.Service
{
    public class SynchronisationService
    {
        /// <summary>
        /// Méthode de vérification de la présence sur le réseau de l'ordinateur ou est installé l'application
        /// </summary>
        /// <param name="adressIp"></param>
        /// <returns></returns>
        public bool IsNetworkConnected(string adressIp)
        {
            bool connected = NetworkInterface.GetIsNetworkAvailable();
            if (connected)
            {
                var a = new Ping();
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();

                // Use the default Ttl value which is 128,
                // but change the fragmentation behavior.
                options.DontFragment = true;

                // Create a buffer of 32 bytes of data to be transmitted.
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;
                PingReply reply = pingSender.Send(adressIp, timeout, buffer, options);
                if (reply.Status != IPStatus.Success)
                {
                    connected = false;
                }
            }

            return connected;
        }

        /// <summary>
        /// Lance la synchronisation entre les deux bases de données
        /// </summary>
        public void SynchronisationVisite()
        {
            ArchivageService.Archivage();

            this.SynchronisationAjout();

            this.SynchronisationSuppression();

            this.SynchronisationModification();

            this.RapatriementBaseCentrale();
        }

        /// <summary>
        /// Ajoute les nouvelles visites à la base centrale
        /// </summary>
        private void SynchronisationAjout()
        {
            var visiteToAdd = new List<Visite>();
            var repoVisiteCentrale = new VisiteRepositoryCentrale();
            var repoVisiteLocale = new VisiteRepository();

            var visitesCentrales = repoVisiteCentrale.GetAll();
            var visitesLocales = repoVisiteLocale.GetAll();

            foreach (var visiteLocale in visitesLocales)
            {
                var matchs = visitesCentrales.Where(w => w.Guid == visiteLocale.Guid).ToList();
                if (matchs.IsNullOrEmpty())
                {
                    visiteToAdd.Add(visiteLocale);
                }
            }

            repoVisiteCentrale.AddRange(visiteToAdd);
        }

        /// <summary>
        /// Supprime les visites de la base centrale
        /// </summary>
        private void SynchronisationSuppression()
        {
            var repoVisiteCentrale = new VisiteRepositoryCentrale();
            var repoVisiteLocale = new VisiteRepository();

            // Pour la suppresion, un champ de la ligne de visite est setter a true, il n'y as pas de vrai suppresion pour éviter
            // les conflits avec les possibles autres modifications, un système de purge est mis en place pour éviter ça
            var visitesLocales = repoVisiteLocale.GetDeleted();
            repoVisiteCentrale.FakeDelete(visitesLocales);
        }

        /// <summary>
        /// Modifie les visites de la base centrale
        /// </summary>
        private void SynchronisationModification()
        {
            var repoVisiteCentrale = new VisiteRepositoryCentrale();
            var repoVisiteLocale = new VisiteRepository();

            var visitesCentrales = repoVisiteCentrale.GetAll();

            var visitesLocales = repoVisiteLocale.GetWithAnUpdateDate();

            foreach (var visite in visitesLocales)
            {
                var temp = visitesCentrales.FirstOrDefault(w => w.Guid == visite.Guid);
                if (temp != null)
                {
                    //DateTime fromLocale = DateTime.Parse();
                    //DateTime fromCentrale = DateTime.Parse();
                    if (temp.DateUpdate.HasValue)
                    {
                        int result = DateTime.Compare(visite.DateUpdate.Value, temp.DateUpdate.Value);

                        // Si la date locale est supérieur à la date centrale
                        if (result > 0)
                        {
                            repoVisiteCentrale.Update(visite);
                        }
                    }
                    else
                    {
                        repoVisiteCentrale.Update(visite);
                    }

                }
            }
        }

        /// <summary>
        /// Recopie toutes les visites de la base centrale dans la base locale
        /// </summary>
        private void RapatriementBaseCentrale()
        {
            var repoVisiteCentrale = new VisiteRepositoryCentrale();
            var repoVisiteLocale = new VisiteRepository();            

            repoVisiteLocale.DeleteAll();

            repoVisiteLocale.AddRange(repoVisiteCentrale.GetAll());
        }
    }
}
