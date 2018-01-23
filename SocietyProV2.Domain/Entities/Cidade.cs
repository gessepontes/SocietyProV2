using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocietyProV2.Domain.Entities
{
    public class Cidade
    {
        public Cidade()
        {
        }

        public int ID { get; set; }

        [StringLength(200)]
        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Nome")]
        public string NOME { get; set; }
        public virtual ICollection<Campo> Campo { get; set; }
    }
}
