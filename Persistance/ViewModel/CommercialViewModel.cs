using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using MVVM;
using Persistance.Data.Entities;
using Persistance.Data.Repositories;
using Persistance.Model;

namespace Persistance.ViewModel
{
    public class CommercialViewModel : ObservableObject
    {
        #region Fields

        private Visibility affichageView;
        private string nomUser;
        private string prenomUser;
        private string fonctionUser;
        private string nomMagasin;
        private DateTime selectedDate;
        private string nomManagerUser;
        private ObservableCollection<Magasin> lesMagasins;
        private ObservableCollection<VisiteModel> lesVisites;
        private MagasinRepository magasinRepository;
        private UtilisateurRepository utilisateurRepository;
        private VisiteRepository visiteRepository;
        private Utilisateur Utilisateur;
        private Magasin selectedMagasin;
        private VisiteModel selectedVisite;
        private RelayCommand supprimerCommand;
        private RelayCommand ajouterCommand;
        private RelayCommand modifierCommand;

        #endregion

        #region Properties

        public Visibility AffichageView { get => affichageView; set => SetProperty(nameof(AffichageView), ref affichageView, value); }
        public string NomUser { get => nomUser; set => SetProperty(nameof(NomUser), ref nomUser, value); }
        public string PrenomUser { get => prenomUser; set => SetProperty(nameof(PrenomUser), ref prenomUser, value); }
        public string FonctionUser { get => fonctionUser; set => SetProperty(nameof(FonctionUser), ref fonctionUser, value); }
        public string NomManagerUser { get => nomManagerUser; set => SetProperty(nameof(NomManagerUser), ref nomManagerUser, value); }
        public ObservableCollection<Magasin> LesMagasins { get => lesMagasins; set => SetProperty(nameof(LesMagasins), ref lesMagasins, value); }
        public Magasin SelectedMagasin
        {
            get
            {
                if (selectedMagasin != null)
                {
                    NomMagasin = selectedMagasin.NomMagasin;
                    List<Visite> visites = visiteRepository.GetVisiteByMagasin(selectedMagasin.IdMagasin);
                    LesVisites = new ObservableCollection<VisiteModel>(this.TransformVisiteForDisplay(visites));
                }

                return selectedMagasin;
            }

            set => SetProperty(nameof(SelectedMagasin), ref selectedMagasin, value);
        }

        public string NomMagasin { get => nomMagasin; set => SetProperty(nameof(NomMagasin), ref nomMagasin, value); }
        public ObservableCollection<VisiteModel> LesVisites { get => lesVisites; set => SetProperty(nameof(LesVisites), ref lesVisites, value); }
        public VisiteModel SelectedVisite { get => selectedVisite; set => SetProperty(nameof(SelectedVisite), ref selectedVisite, value); }
        public RelayCommand SupprimerCommand { get => supprimerCommand; set => supprimerCommand = value; }
        public RelayCommand AjouterCommand { get => ajouterCommand; set => ajouterCommand = value; }
        public RelayCommand ModifierCommand { get => modifierCommand; set => modifierCommand = value; }
        public DateTime SelectedDate { get => selectedDate; set => SetProperty(nameof(SelectedDate), ref selectedDate, value); }

        #endregion

        public CommercialViewModel()
        {
            AffichageView = Visibility.Hidden;
            magasinRepository = new MagasinRepository();
            utilisateurRepository = new UtilisateurRepository();
            visiteRepository = new VisiteRepository();
            SupprimerCommand = new RelayCommand(SupprimerCommandExecute);
            AjouterCommand = new RelayCommand(AjouterCommandExecute);
            ModifierCommand = new RelayCommand(ModifierCommandExecute);
        }

        private void ModifierCommandExecute(object obj)
        {
            throw new NotImplementedException();
        }

        private void AjouterCommandExecute(object obj)
        {
            if (SelectedDate != null && string.IsNullOrEmpty(NomMagasin))
            {
                Visite visite = new Visite();
                visite.DateHeureVisite = SelectedDate.ToString();
                visite.IdUtilisateur = Utilisateur.IdUtilisateur;
                visite.IdMagasin = magasinRepository.GetMagasinByNom(NomMagasin).IdMagasin;

                // TODO: Continuer
            }
        }

        private void SupprimerCommandExecute(object obj)
        {
            throw new NotImplementedException();
        }

        public void SetUtilisateur(Utilisateur utilisateur)
        {
            this.Utilisateur = utilisateur;
        }

        public void UpdateUser()
        {
            NomUser = Utilisateur.Nom;
            PrenomUser = Utilisateur.Prenom;
            FonctionUser = Utilisateur.Fonction;
            NomManagerUser = Utilisateur.NomManager;
        }

        public void UpdateMagasin()
        {
            List<Magasin> magasins = magasinRepository.GetMagasinByUtilisateur(Utilisateur);
            LesMagasins = new ObservableCollection<Magasin>(magasins);
        }

        public List<VisiteModel> TransformVisiteForDisplay(List<Visite> visites)
        {
            List<VisiteModel> visiteModels = new List<VisiteModel>();
            foreach (var visite in visites)
            {
                VisiteModel visiteModel = new VisiteModel();
                var user = utilisateurRepository.GetUtilisateur(visite.IdUtilisateur);
                visiteModel.Commercial = user.Prenom + " " + user.Nom;
                visiteModel.DateEtHeure = visite.DateHeureVisite;
                visiteModel.Magasin = magasinRepository.GetMagasin(visite.IdMagasin).NomMagasin;
                visiteModel.VisiteRealise = visite.VisiteRealise ? "Non" : "Oui";
                visiteModel.Id = visite.IdVisite;
                visiteModels.Add(visiteModel);
            }

            return visiteModels;
        }

        public Visite TransformBackToVisite(VisiteModel visiteModel)
        {
            return visiteRepository.GetVisite(visiteModel.Id);
        }
    }
}
