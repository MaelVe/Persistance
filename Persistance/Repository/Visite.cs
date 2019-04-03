using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistance.Repository
{
    [Table("Visite")]
    public class Visite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdVisite { get; set; }

        [Required]
        public string DateHeureVisite { get; set; }

        public bool VisiteRealise { get; set; }

        public int IdUtilisateur { get; set; }

        public int IdMagasin { get; set; }
    }
}
