using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocietyProV2.Domain.Entities
{
    public class Inscricao
    {
       public Inscricao() {
            bAtivo = true;
            dDataCadastro = DateTime.Now;
        }

        public int ID { get; set; }

        [Display(Name = "Time")]
        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        public int IDTime { get; set; }

        public int IDCampeonato { get; set; }

        public bool bAtivo { get; set; }
        public DateTime dDataCadastro { get; set; }

        public virtual Campeonato Campeonato { get; set; }

        public virtual Time Time { get; set; }

        public virtual ICollection<Grupo> Grupo { get; set; }

        public virtual ICollection<JogadorInscrito> JogadorInscrito { get; set; }
        
        public virtual ICollection<PartidaCampeonato> PartidaCampeonato { get; set; }
    }
}
