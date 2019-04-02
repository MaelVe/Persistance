using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MVVM;

namespace Persistance.ViewModel
{
    public class CommercialViewModel : ObservableObject
    {
        private Visibility affichageView;
        public Visibility AffichageView { get => affichageView; set => SetProperty(nameof(AffichageView), ref affichageView, value); }       


        private static CommercialViewModel instance = null;
        private static readonly object padlock = new object();

        public CommercialViewModel()
        {
            AffichageView = Visibility.Hidden;
        }

        public static CommercialViewModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new CommercialViewModel();
                    }
                    return instance;
                }
            }
        }
    }
}
