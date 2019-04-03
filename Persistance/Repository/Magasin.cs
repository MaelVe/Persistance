using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistance.Repository
{
    [Table("Magasin")]
    public class Magasin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMagasin { get; set; }

        [Required]
        public string NomMagasin { get; set; }

        [Required]
        public string Enseigne { get; set; }

    }
}
