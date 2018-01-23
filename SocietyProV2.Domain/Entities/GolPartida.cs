using System;
using System.ComponentModel.DataAnnotations;

namespace SocietyProV2.Domain.Entities
{
    public class GolPartida
    {
        public GolPartida()
        {
            DATACADASTRO = DateTime.Now;
            STATUS = true;
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Jogador")]
        public int IDJOGADORPARTIDA { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Qtd Gols")]
        public int QTDGOL { get; set; }

        public bool STATUS { get; set; }

        public DateTime DATACADASTRO { get; set; }

        public virtual JogadorPartida JogadorPartida { get; set; }
    }
}
