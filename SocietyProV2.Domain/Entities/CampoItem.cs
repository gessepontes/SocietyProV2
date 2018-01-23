using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocietyProV2.Domain.Entities
{
    public class CampoItem
    {
        public CampoItem()
        {
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Campo")]
        public int IDCAMPO { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Descrição")]
        [StringLength(50)]
        public string DESCRICAO { get; set; }

        [Display(Name = "Ativo")]
        public bool ATIVO { get; set; }

        [Display(Name = "Campo")]
        public virtual Campo Campo { get; set; }

        public virtual ICollection<CampoValor> CampoValor { get; set; }
        public virtual ICollection<Horario> Horario { get; set; }
        public virtual ICollection<HorarioExtra> HorarioExtra { get; set; }

    }
}
