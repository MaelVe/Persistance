using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using MVVM;
using Persistance.Data.Entities;
using Persistance.Data.Repositories;
using Persistance.Model;
using Persistance.Service;

namespace Persistance.ViewModel
{
    /// <summary>
    /// ViewModel de la fenêtre principale
    /// </summary>
    public class CommercialViewModel : ObservableObject
    {
        #region Fields

        private Visibility affichageView;
        private string nomUser;
        private string prenomUser;
        private string fonctionUser;
        private string nomMagasin;
        private int rowStackSuppr;
        private bool visiteRealiseAjout;
        private bool temp;
        private bool IsManager;
        private bool isEnabled;
        private bool commercialAffichage;
        private bool visiteRealiseModif;
        private DateTime selectedDateAjout;
        private DateTime selectedDateModif;
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
        private RelayCommand synchroCommand;


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
                    this.UpdateVisite();
                }

                return selectedMagasin;
            }
            set => SetProperty(nameof(SelectedMagasin), ref selectedMagasin, value);
        }
        public string NomMagasin { get => nomMagasin; set => SetProperty(nameof(NomMagasin), ref nomMagasin, value); }
        public ObservableCollection<VisiteModel> LesVisites { get => lesVisites; set => SetProperty(nameof(LesVisites), ref lesVisites, value); }
        public VisiteModel SelectedVisite
        {
            get
            {               
                IsEnabled = false;
                if (selectedVisite != null)
                {
                    if (IsManager)
                    {
                        IsEnabled = true;
                        return selectedVisite;
                    }

                    VisiteRealiseModif = selectedVisite.VisiteRealise == "Oui" ? true : false;
                    SelectedDateModif = selectedVisite.DateEtHeure;
                    var commercial = selectedVisite.Commercial.Split(' ');
                    if (commercial[0] == Utilisateur.Prenom && commercial[1] == Utilisateur.Nom)
                    {
                        IsEnabled = true;
                    }
                }
                return selectedVisite;
            }
            set => SetProperty(nameof(SelectedVisite), ref selectedVisite, value);
        }
        public RelayCommand SupprimerCommand { get => supprimerCommand; set => supprimerCommand = value; }
        public RelayCommand AjouterCommand { get => ajouterCommand; set => ajouterCommand = value; }
        public RelayCommand ModifierCommand { get => modifierCommand; set => modifierCommand = value; }
        public DateTime SelectedDateAjout { get => selectedDateAjout; set => SetProperty(nameof(SelectedDateAjout), ref selectedDateAjout, value); }
        public bool VisiteRealiseAjout { get => visiteRealiseAjout; set => SetProperty(nameof(VisiteRealiseAjout), ref visiteRealiseAjout, value); }
        public DateTime SelectedDateModif { get => selectedDateModif; set => SetProperty(nameof(SelectedDateModif), ref selectedDateModif, value); }
        public bool VisiteRealiseModif
        {
            get => visiteRealiseModif;
            set
            {
                temp = visiteRealiseModif;
                SetProperty(nameof(VisiteRealiseModif), ref visiteRealiseModif, value);
            }
        }
        public bool IsEnabled { get => isEnabled; set => SetProperty(nameof(IsEnabled), ref isEnabled, value); }
        public bool CommercialAffichage { get => commercialAffichage; set => SetProperty(nameof(CommercialAffichage), ref commercialAffichage, value); }
        public int RowStackSuppr { get => rowStackSuppr; set => SetProperty(nameof(RowStackSuppr), ref rowStackSuppr, value); }
        public RelayCommand SynchroCommand { get => synchroCommand; set => synchroCommand = value; }

        #endregion

        /// <summary>
        /// Constructeur principale de la classe
        /// </summary>
        public CommercialViewModel()
        {
            IsEnabled = false;
            AffichageView = Visibility.Hidden;
            magasinRepository = new MagasinRepository();
            utilisateurRepository = new UtilisateurRepository();
            visiteRepository = new VisiteRepository();
            SupprimerCommand = new RelayCommand(SupprimerCommandExecute);
            AjouterCommand = new RelayCommand(AjouterCommandExecute);
            ModifierCommand = new RelayCommand(ModifierCommandExecute);
            SynchroCommand = new RelayCommand(SynchroCommandExecute);
        }

        /// <summary>
        /// Action lors de l'appui sur le bouton de synchro
        /// </summary>
        /// <param name="obj"></param>
        private void SynchroCommandExecute(object obj)
        {
            var synchro = new SynchronisationService();
            if (synchro.IsNetworkConnected("127.0.0.1"))
            {
                synchro.SynchronisationVisite();
                this.UpdateVisite();
            }
        }

        /// <summary>
        /// Action lors de l'appui sur le bouton de modification
        /// </summary>
        /// <param name="obj"></param>
        private void ModifierCommandExecute(object obj)
        {
            if (SelectedDateModif != null)
            {
                try
                {
                    var dateModifTemp = SelectedDateModif.ToString();
                    var visite = this.TransformBackToVisite(SelectedVisite);
                    visite.DateHeureVisite = DateTime.Parse(dateModifTemp);
                    visite.VisiteRealise = temp;
                    visite.DateUpdate = DateTime.Now;
                    visiteRepository.Update(visite);
                    this.UpdateVisite();
                }
                catch (Exception ex)
                {

                }
            }
        }

        /// <summary>
        /// Action lors de l'appui sur le bouton d'ajout
        /// </summary>
        /// <param name="obj"></param>
        private void AjouterCommandExecute(object obj)
        {
            if (SelectedDateAjout != null && !string.IsNullOrEmpty(NomMagasin))
            {
                Visite visite = new Visite();
                visite.DateHeureVisite = SelectedDateAjout;
                visite.IdUtilisateur = Utilisateur.IdUtilisateur;
                visite.IdMagasin = magasinRepository.GetMagasinByNom(NomMagasin).IdMagasin;
                visite.VisiteRealise = VisiteRealiseAjout;
                visite.Guid = Guid.NewGuid().ToString();
                visite.IsDelete = false;
                visiteRepository.Add(visite);
                this.UpdateVisite();
            }
        }

        /// <summary>
        /// Action lors de l'appui sur le bouton de suppression
        /// </summary>
        /// <param name="obj"></param>
        private void SupprimerCommandExecute(object obj)
        {
            if (SelectedVisite != null)
            {
                var visite = this.TransformBackToVisite(SelectedVisite);
                visite.IsDelete = true;
                visiteRepository.Update(visite);
                this.UpdateVisite();
            }
        }

        /// <summary>
        /// Fonction de mise a jour de la liste visible dans l'IHM concernant les visites d'un magasin
        /// </summary>
        private void UpdateVisite()
        {
            List<Visite> visites = visiteRepository.GetVisiteByMagasin(selectedMagasin.IdMagasin);
            LesVisites = new ObservableCollection<VisiteModel>(this.TransformVisiteForDisplay(visites));
        }

        /// <summary>
        /// Récupération de l'utilisateur depuis la fenêtre de login et afichage des cards en fonction de la fonction de l'utilisateur
        /// </summary>
        /// <param name="utilisateur"></param>
        public void SetUtilisateur(Utilisateur utilisateur)
        {
            this.Utilisateur = utilisateur;
            if (utilisateur.Fonction == "Manager")
            {
                IsManager = true;
                CommercialAffichage = false;
                RowStackSuppr = 1;
            }
            else
            {
                IsManager = false;
                CommercialAffichage = true;
                RowStackSuppr = 3;
            }
        }

        /// <summary>
        /// Remplissage des champs concernant les information sur l'utilisateur
        /// </summary>
        public void UpdateUser()
        {
            NomUser = Utilisateur.Nom;
            PrenomUser = Utilisateur.Prenom;
            FonctionUser = Utilisateur.Fonction;
            NomManagerUser = Utilisateur.NomManager;
        }

        /// <summary>
        /// Met à jour la liste des magasins
        /// </summary>
        public void UpdateMagasin()
        {
            List<Magasin> magasins;
            if (IsManager)
            {
                magasins = magasinRepository.GetAll();
                LesMagasins = new ObservableCollection<Magasin>(magasins);
            }
            else
            {
                magasins = magasinRepository.GetMagasinByUtilisateur(Utilisateur);
                LesMagasins = new ObservableCollection<Magasin>(magasins);
            }          
        }

        /// <summary>
        /// Transforme l'entité Visite en objet VisiteModel pour affichage
        /// </summary>
        /// <param name="visites"></param>
        /// <returns></returns>
        public List<VisiteModel> TransformVisiteForDisplay(List<Visite> visites)
        {
            List<VisiteModel> visiteModels = new List<VisiteModel>();
            foreach (var visite in visites)
            {
                VisiteModel visiteModel = new VisiteModel();
                var user = utilisateurRepository.GetUtilisateur(visite.IdUtilisateur);
                visiteModel.Commercial = user.Prenom + " " + user.Nom;
                visiteModel.DateEtHeure = visite.DateHeureVisite.Value;
                visiteModel.Magasin = magasinRepository.GetMagasin(visite.IdMagasin).NomMagasin;
                visiteModel.VisiteRealise = visite.VisiteRealise ? "Oui" : "Non";
                visiteModel.Id = visite.IdVisite;
                visiteModels.Add(visiteModel);
            }

            return visiteModels;
        }

        /// <summary>
        /// Transforme l'objet VisiteModel en entité Visite
        /// </summary>
        /// <param name="visiteModel"></param>
        /// <returns></returns>
        public Visite TransformBackToVisite(VisiteModel visiteModel)
        {
            return visiteRepository.GetVisite(visiteModel.Id);
        }
    }
}
