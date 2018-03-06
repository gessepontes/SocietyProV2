using SocietyProV2.Domain.Diversos;
using System.ComponentModel.DataAnnotations;

namespace SocietyProV2.Domain.Entities
{
    public class Cartao
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Jogador")]
        public int IDJogadorSumula { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Cartão")]
        public TipoCartao iTipoCartao { get; set; }
    }
}
