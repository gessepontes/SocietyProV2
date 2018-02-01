using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocietyProV2.Domain.Entities
{
    public class PreInscricao
    {
        public int ID { get; set; }

        [Display(Name = "Time")]
        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        public int IDTime { get; set; }

        public int IDCampeonato { get; set; }

        public virtual Campeonato Campeonato { get; set; }

        public virtual Time Time { get; set; }

        [Display(Name = "Inscrito")]
        public virtual ICollection<Inscricao> Inscrito { get; set; }

    }
}
