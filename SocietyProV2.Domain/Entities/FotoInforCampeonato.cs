using System.ComponentModel.DataAnnotations;

namespace SocietyProV2.Domain.Entities
{
    public class FotoInforCampeonato
    {
        public int ID { get; set; }

        [Display(Name = "Nome")]
        [StringLength(100)]
        public string NOME { get; set; }
        public string FOTO { get; set; }

        public int ISEQUENCIA { get; set; }

        public int IDCAMPEONATO { get; set; }
        public virtual Campeonato Campeonato { get; set; }
    }
}
