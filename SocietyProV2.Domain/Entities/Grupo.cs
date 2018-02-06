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

    public enum IDGrupo : int
    {
        A = 1,
        B = 2,
        C = 3,
        D = 4,
        E = 5,
        F = 6,
        G = 7,
        H = 8,
        I = 9,
        J = 10,
        L = 11,
        M = 12
    }
}
