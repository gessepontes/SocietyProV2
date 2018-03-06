using SocietyProV2.Domain.Diversos;
using System.ComponentModel.DataAnnotations;

namespace SocietyProV2.Domain.Entities
{
    public class Horario
    {
        public Horario()
        {
            STATUS = true;
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Campo")]
        public int IDITEMCAMPO { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Horário")]
        [StringLength(10)]
        public string HORARIO { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Dia da Semana")]
        public DiaSemana DIASEMANA { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Status")]
        public bool STATUS { get; set; }

        [Display(Name = "Campo Item")]
        public virtual CampoItem CampoItem { get; set; }

    }
}
