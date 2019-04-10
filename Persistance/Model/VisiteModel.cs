using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Model
{
    /// <summary>
    /// Classe servant de modele pour l'affichage dans l'ihm
    /// </summary>
    public class VisiteModel
    {
        private int id;
        private string commercial;
        private string magasin;
        private DateTime dateEtHeure;
        private string visiteRealise;

        public string Commercial { get => commercial; set => commercial = value; }
        public string Magasin { get => magasin; set => magasin = value; }
        public DateTime DateEtHeure { get => dateEtHeure; set => dateEtHeure = value; }
        public string VisiteRealise { get => visiteRealise; set => visiteRealise = value; }
        public int Id { get => id; set => id = value; }
    }
}
