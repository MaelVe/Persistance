using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Data.Entities;

namespace Persistance.Data.Repositories
{
    /// <summary>
    /// Classe regroupant les méthodes CRUD sur une table de la base de données
    /// </summary>
    public class VisiteRepository
    {
        private readonly PersistanceDbContext context;

        public VisiteRepository()
        {
            context = new PersistanceDbContext();
        }

        /// <summary>
        /// Ajoute une visite dans la base
        /// </summary>
        /// <param name="visite">la visite a ajouter</param>
        public void Add(Visite visite)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    
                    context.Visites.Add(visite);

                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Ajoute une liste de Visite dans la base de données
        /// </summary>
        /// <param name="commercialMagasins">une liste de Visite</param>
        public void AddRange(List<Visite> visites)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    context.Visites.AddRange(visites);

                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Met à jour une visite dans la base
        /// </summary>
        /// <param name="visite">la visite à mettre a jour</param>
        public void Update(Visite visite)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var entity = context.Visites.Find(visite.IdVisite);
                    if (entity == null)
                    {
                        return;
                    }

                    context.Entry(entity).CurrentValues.SetValues(visite);
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Supprime une visite de la base
        /// </summary>
        /// <param name="visite">la visite à supprimer</param>
        public void Remove(Visite visite)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var entity = context.Visites.Remove(visite);                   
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Récupère une visite de la base grâce à son id
        /// </summary>
        /// <param name="idVisite">l'id de la visite</param>
        /// <returns>une visite</returns>
        public Visite GetVisite(int idVisite)
        {
            return context.Visites.FirstOrDefault(f => f.IdVisite == idVisite);
        }

        /// <summary>
        /// Récupère une liste visite de la base grâce l'id d'un magasin
        /// </summary>
        /// <param name="idMagasin">l'id du magasin</param>
        /// <returns>la liste de visite</returns>
        public List<Visite> GetVisiteByMagasin(int idMagasin)
        {
            return context.Visites.Where(w => w.IdMagasin == idMagasin && w.IsDelete == false).ToList();
        }

        /// <summary>
        /// Récupère toutes les données de la table Visite
        /// </summary>
        /// <returns>Une liste de Visite</returns>
        public List<Visite> GetAll()
        {
            return context.Visites.ToList();
        }

        /// <summary>
        /// Récupère toutes les visites qui ne sont pas indiquées comme supprimées
        /// </summary>
        /// <returns>Une liste de Visite</returns>
        public List<Visite> GetNotDeleted()
        {
            return context.Visites.Where(w => w.IsDelete == false).ToList();
        }

        /// <summary>
        /// Récupère toutes les visites qui sont indiquées comme supprimées
        /// </summary>
        /// <returns>Une liste de Visite</returns>
        public List<Visite> GetDeleted()
        {
            return context.Visites.Where(w => w.IsDelete == true).ToList();
        }

        /// <summary>
        /// Récupère toutes les visites qui ont une date de modification
        /// </summary>
        /// <returns>Une liste de Visite</returns>
        public List<Visite> GetWithAnUpdateDate()
        {
            return context.Visites.Where(w => w.DateUpdate != null).ToList();
        }

        /// <summary>
        /// Supprime toutes les visites
        /// </summary>
        public void DeleteAll()
        {            
            context.Visites.RemoveRange(this.GetAll());
        }
    }
}
