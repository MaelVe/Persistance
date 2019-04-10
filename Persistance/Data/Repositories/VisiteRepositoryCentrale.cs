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
    public class VisiteRepositoryCentrale
    {
        private readonly PersistanceDbContextCentrale context;

        public VisiteRepositoryCentrale()
        {
            context = new PersistanceDbContextCentrale();
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
        /// Ajoute une liste de Visite dans la base de données centrale
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
        /// Supprime plusieurs visites de la base
        /// </summary>
        /// <param name="visite">les visites à supprimer</param>
        public void RemoveRange(List<Visite> visites)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var visite in visites)
                    {
                        var entity = context.Visites.Remove(visite);
                    }

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
        public List<Visite> GetDeleted()
        {
            return context.Visites.Where(w => w.IsDelete == true).ToList();
        }

       /// <summary>
       /// Set le champ IsDeleted a true pour une liste de visites
       /// </summary>
       /// <param name="visites">la liste de visites</param>
        public void FakeDelete(List<Visite> visites)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var visite in visites)
                    {
                        var entity = context.Visites.Find(visite.IdVisite);
                        if (entity == null)
                        {
                            return;
                        }

                        context.Entry(entity).CurrentValues.SetValues(visite);
                    }

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
    }
}
