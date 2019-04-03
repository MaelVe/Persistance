using System.Windows;
using MVVM;

namespace Persistance.ViewModel
{
    public class CommercialViewModel : ObservableObject
    {
        private Visibility affichageView;
        public Visibility AffichageView { get => affichageView; set => SetProperty(nameof(AffichageView), ref affichageView, value); }       

        public CommercialViewModel()
        {
            AffichageView = Visibility.Hidden;
        }      
    }
}
