using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MVVM;

namespace Persistance.ViewModel
{
    public class StartPageViewModel : ObservableObject
    {
        #region Fields

        private List<string> role;
        private RelayCommand validerCommand;
        private string selectedRole;
        private Visibility affichageLogin;
        private CommercialViewModel CommercialViewModel;

        #endregion

        #region Properties

        public List<string> Role { get => role; set => SetProperty(nameof(Role), ref role, value); }
        public RelayCommand ValiderCommand { get => validerCommand; set => validerCommand = value; }
        public string SelectedRole { get => selectedRole; set => SetProperty(nameof(SelectedRole), ref selectedRole, value); }    
        public Visibility AffichageLogin { get => affichageLogin; set => SetProperty(nameof(AffichageLogin), ref affichageLogin, value); }


        #endregion

        public StartPageViewModel(CommercialViewModel commercialViewModel)
        {
            Role = new List<string>();
            Role.Add("Commercial");
            Role.Add("Manager");

            ValiderCommand = new RelayCommand(ValiderCommandExecute);
            this.CommercialViewModel = commercialViewModel;
        }

        /// <summary>
        /// Méthode d'éxécution
        /// </summary>
        /// <param name="obj"></param>
        public void ValiderCommandExecute(object obj)
        {
            this.AffichageLogin = Visibility.Hidden;
            this.CommercialViewModel.AffichageView = Visibility.Visible;
        }
    }
}
