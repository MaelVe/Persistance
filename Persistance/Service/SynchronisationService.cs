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

        public void SynchronisationVisite()
        {
            #region temp

            //var magasinRepositoryCentrale = new MagasinRepositoryCentrale();
            //var magasinRepository = new MagasinRepository();

            //var utilisateurRepositoryCentrale = new UtilisateurRepositoryCentrale();
            //var utilisateurRepository = new UtilisateurRepository();

            //var commercialMagasinRepositoryCentrale = new CommercialMagasinRepositoryCentrale();
            //var commercialMagasinRepository = new CommercialMagasinRepository();

            ////magasinRepositoryCentrale.AddRange(magasinRepository.GetAll());
            //var temp = utilisateurRepository.GetAll();
            //utilisateurRepositoryCentrale.AddRange(temp);

            //commercialMagasinRepositoryCentrale.AddRange(commercialMagasinRepository.GetAll());


            #endregion

            this.SynchronisationAjout();

            this.SynchronisationSuppression();

            this.SynchronisationModification();

            this.RapatriementBaseCentrale();
        }

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

        private void SynchronisationSuppression()
        {
            var repoVisiteCentrale = new VisiteRepositoryCentrale();
            var repoVisiteLocale = new VisiteRepository();

            //var visitesCentrales = repoCentrale.GetDeleted();
            var visitesLocales = repoVisiteLocale.GetDeleted();
            repoVisiteCentrale.FakeDelete(visitesLocales);
        }

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

        private void RapatriementBaseCentrale()
        {
            var repoVisiteCentrale = new VisiteRepositoryCentrale();
            var repoVisiteLocale = new VisiteRepository();

            ArchivageService.Archivage();

            repoVisiteLocale.DeleteAll();

            repoVisiteLocale.AddRange(repoVisiteCentrale.GetAll());
        }
    }
}
