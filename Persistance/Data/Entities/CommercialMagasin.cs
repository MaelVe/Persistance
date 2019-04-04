using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistance.Data.Entities
{
    [Table("CommercialMagasin")]
    public class CommercialMagasin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCommercialMagasin { get; set; }

        [Required]
        public int IdUtilisateur { get; set; }

        [Required]
        public int IdMagasin { get; set; }
    }
}
