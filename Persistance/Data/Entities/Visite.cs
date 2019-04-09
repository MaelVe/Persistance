using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistance.Data.Entities
{
    [Table("Visite")]
    public class Visite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdVisite { get; set; }

        [Required]
        public DateTime? DateHeureVisite { get; set; }

        public DateTime? DateUpdate { get; set; }

        public string Guid { get; set; }

        public bool VisiteRealise { get; set; }

        public int IdUtilisateur { get; set; }

        public int IdMagasin { get; set; }

        public bool IsDelete { get; set; }
    }
}
