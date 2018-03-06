using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocietyProV2.Domain.Entities
{
    public class JogadorInscrito
    {
        public JogadorInscrito()
        {
            STATUS = "A";
            dDataCadastro = DateTime.Now;
        }

        public int ID { get; set; }

        public int IDJogador { get; set; }

        public int IDInscrito { get; set; }

        public string STATUS { get; set; }

        [Display(Name = "Data de Inscrição")]
        public DateTime? dDataCadastro { get; set; }
        public DateTime? dDataDispensa { get; set; }

        public virtual ICollection<JogadorPartidaCampeonato> JogadorPartidaCampeonato { get; set; }

        public virtual Inscricao Inscricao { get; set; }

        public virtual Jogador Jogador { get; set; }
    }
}
