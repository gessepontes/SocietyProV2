using SocietyProV2.Domain.Diversos;
using System;
using System.ComponentModel.DataAnnotations;

namespace SocietyProV2.Domain.Entities
{
    public class Grupo
    {
        public Grupo()
        {
            dDataCadastro = DateTime.Now;
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Grupo")]
        public IDGrupo IDGrupo { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Time")]
        public int IDInscrito { get; set; }

        public DateTime dDataCadastro { get; set; }

        public virtual Inscricao Inscricao { get; set; }
    }
}
