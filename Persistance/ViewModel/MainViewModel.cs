using MVVM;

namespace Persistance.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private StartPageViewModel startPageViewModel;
        private CommercialViewModel commercialViewModel;
        private int height;
        private int width;

        public MainViewModel()
        {
            Height = 500;
            Width = 300;
            this.CommercialViewModel = new CommercialViewModel();
            this.StartPageViewModel = new StartPageViewModel(this.CommercialViewModel, this);
        }

        public StartPageViewModel StartPageViewModel { get => startPageViewModel; set => SetProperty(nameof(StartPageViewModel), ref startPageViewModel, value);}
        public CommercialViewModel CommercialViewModel { get => commercialViewModel; set => SetProperty(nameof(CommercialViewModel), ref commercialViewModel, value); }
        public int Height { get => height; set => SetProperty(nameof(Height), ref height, value); }
        public int Width { get => width; set => SetProperty(nameof(Width), ref width, value); }
    }
}
