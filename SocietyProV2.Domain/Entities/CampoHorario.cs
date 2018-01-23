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

    public enum DiaSemana : int
    {
        [Display(Name = "Segunda-feira")]
        Segunda = 1,
        [Display(Name = "Terça-feira")]
        Terça = 2,
        [Display(Name = "Quarta-feira")]
        Quarta = 3,
        [Display(Name = "Quinta-feira")]
        Quinta = 4,
        [Display(Name = "Sexta-feira")]
        Sexta = 5,
        [Display(Name = "Sabado")]
        Sabado = 6,
        [Display(Name = "Domingo")]
        Domingo = 0
    }
}
