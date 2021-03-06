﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistance.Data.Entities
{

    [Table("Utilisateur")]
    public class Utilisateur
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUtilisateur { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Prenom { get; set; }

        [Required]
        public string Fonction { get; set; }
        
        public string NomManager { get; set; }

    }
}
