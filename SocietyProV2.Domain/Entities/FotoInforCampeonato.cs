using System.ComponentModel.DataAnnotations;

namespace SocietyProV2.Domain.Entities
{
    public class FotoInforCampeonato
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Nome")]
        [StringLength(100)]
        public string NOME { get; set; }

        [Display(Name = "Foto")]
        public string FOTO { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Sequencia")]
        public int ISEQUENCIA { get; set; }

        public int IDCAMPEONATO { get; set; }
        public virtual Campeonato Campeonato { get; set; }
    }
}
