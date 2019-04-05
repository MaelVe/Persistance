using System;
using System.Collections.Generic;
using System.Windows;
using MVVM;
using Persistance.Data.Entities;
using Persistance.Data.Repositories;

namespace Persistance.ViewModel
{
    public class StartPageViewModel : ObservableObject
    {
        #region Fields
        
        private RelayCommand validerCommand;
        private string selectedRole;
        private Visibility affichageLogin;
        private CommercialViewModel CommercialViewModel;
        private MainViewModel MainViewModel;
        private VisiteRepository visiteRepository;
        private UtilisateurRepository utilisateurRepository;
        private string prenom;
        private string nom;
        private bool errorMessageVisibility;
        private string errorMessage;           

        #endregion

        #region Properties

        public RelayCommand ValiderCommand { get => validerCommand; set => validerCommand = value; }
        public string SelectedRole { get => selectedRole; set => SetProperty(nameof(SelectedRole), ref selectedRole, value); }    
        public Visibility AffichageLogin { get => affichageLogin; set => SetProperty(nameof(AffichageLogin), ref affichageLogin, value); }
        public string Prenom { get => prenom; set => SetProperty(nameof(Prenom), ref prenom, value); }
        public string Nom { get => nom; set => SetProperty(nameof(Nom), ref nom, value); }
        public bool ErrorMessageVisibility { get => errorMessageVisibility; set => SetProperty(nameof(ErrorMessageVisibility), ref errorMessageVisibility, value); }
        public string ErrorMessage { get => errorMessage; set => SetProperty(nameof(ErrorMessage), ref errorMessage, value); }


        #endregion

        public StartPageViewModel(CommercialViewModel commercialViewModel, MainViewModel mainViewModel)
        {
            ErrorMessageVisibility = false;
            visiteRepository = new VisiteRepository();
            utilisateurRepository = new UtilisateurRepository();
            this.MainViewModel = mainViewModel;
            ValiderCommand = new RelayCommand(ValiderCommandExecute);
            this.CommercialViewModel = commercialViewModel;
        }

        /// <summary>
        /// Méthode d'éxécution
        /// </summary>
        /// <param name="obj"></param>
        public void ValiderCommandExecute(object obj)
        {
            if(!string.IsNullOrEmpty(Prenom) && !string.IsNullOrEmpty(Nom))
            {
                Utilisateur user = utilisateurRepository.SearchUtilisateur(Nom, Prenom);
                if(user != null)
                {
                    this.CommercialViewModel.SetUtilisateur(user);
                    this.CommercialViewModel.UpdateUser();
                    this.CommercialViewModel.UpdateMagasin();
                    this.AffichageLogin = Visibility.Hidden;
                    this.CommercialViewModel.AffichageView = Visibility.Visible;
                    this.MainViewModel.Height = 1035;
                    this.MainViewModel.Width = 1380;
                }
                else
                {
                    ErrorMessage = "Aucun utilisateur ne possède ce prénom et ce nom !";
                    ErrorMessageVisibility = true;
                }
            }
            else
            {
                ErrorMessage = "Le nom ou le prénom n'est pas renseigné !";
                ErrorMessageVisibility = true;
            }
                   
        }
    }
}