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

        private static MainViewModel instance = null;
        private static readonly object padlock = new object();
        private StartPageViewModel startPageViewModel;
        private CommercialViewModel commercialViewModel;

        public MainViewModel()
        {
            this.StartPageViewModel = new StartPageViewModel();
            this.CommercialViewModel = new CommercialViewModel();
        }
        

        public static MainViewModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new MainViewModel();
                    }
                    return instance;
                }
            }
        }

        public StartPageViewModel StartPageViewModel { get => startPageViewModel; set => SetProperty(nameof(StartPageViewModel), ref startPageViewModel, value);}
        public CommercialViewModel CommercialViewModel { get => commercialViewModel; set => SetProperty(nameof(CommercialViewModel), ref commercialViewModel, value); }
    }
}
