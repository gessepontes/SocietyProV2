using System.ComponentModel.DataAnnotations;

namespace SocietyProV2.Domain.Entities
{
    public class JogadorPartidaCampeonato
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Jogador")]
        public int IDJogadorInscrito { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Camisa")]
        public int iNumCamisa { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Partida")]
        public int? IDPartidaCampeonato { get; set; }
        
        public virtual PartidaCampeonato PartidaCampeonato { get; set; }

        public virtual JogadorInscrito JogadorInscrito { get; set; }

    }
}
