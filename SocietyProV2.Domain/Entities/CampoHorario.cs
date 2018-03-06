using SocietyProV2.Domain.Diversos;
using System;
using System.ComponentModel.DataAnnotations;

namespace SocietyProV2.Domain.Entities
{
    public class CampoHorario
    {
        public CampoHorario()
        {
            DATACADASTRO = DateTime.Now.Date;
            STATUS = true;
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Campo")]
        public int IDCAMPO { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Time")]
        public int IDTIME { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Horário")]
        [StringLength(10)]
        public string HORARIO { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Dia da Semana")]
        public DiaSemana DIASEMANA { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data da Partida")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DATAPARTIDA { get; set; }

        public bool STATUS { get; set; }

        public DateTime DATACADASTRO { get; set; }

        [Display(Name = "Campo")]
        public virtual Campo Campo { get; set; }

        [Display(Name = "Time")]
        public virtual Time Time { get; set; }

    }
}
