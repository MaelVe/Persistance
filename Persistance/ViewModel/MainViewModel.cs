using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MVVM;

namespace Persistance.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private StartPageViewModel startPageViewModel;
        private CommercialViewModel commercialViewModel;

        public MainViewModel()
        {
            this.CommercialViewModel = new CommercialViewModel();
            this.StartPageViewModel = new StartPageViewModel(this.CommercialViewModel);
        }

        public StartPageViewModel StartPageViewModel { get => startPageViewModel; set => SetProperty(nameof(StartPageViewModel), ref startPageViewModel, value);}
        public CommercialViewModel CommercialViewModel { get => commercialViewModel; set => SetProperty(nameof(CommercialViewModel), ref commercialViewModel, value); }
    }
}
