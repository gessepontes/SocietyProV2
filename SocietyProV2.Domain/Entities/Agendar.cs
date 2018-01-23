using System;
using System.ComponentModel.DataAnnotations;

namespace SocietyProV2.Domain.Entities
{
    public class Agendar
    {
        public Agendar()
        {
        }

        public int ID { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Data de Partida")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DATA { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Horário")]
        public int IDHORARIO { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Tipo de Agendamento")]
        public TipoAgendamento TIPO { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Tipo de Horário")]
        public TipoHorario TIPOHORARIO { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Status da Solicitação")]
        [StringLength(1)]
        public string STATUS { get; set; }

        [Display(Name = "Cliente")]
        public int? IDPESSOA { get; set; }

        [Display(Name = "Cliente não cadastrado")]
        [StringLength(100)]
        public string CLIENTENAOCADASTRADO { get; set; }

        [Display(Name = "Telefone")]
        [StringLength(20)]
        public string TELEFONE { get; set; }

        public DateTime DATACADASTRO { get; set; }
        public DateTime? DATACANCELAMENTO { get; set; }
        public int? IDPESSOACANCELAMENTO { get; set; }
        public bool MARCADOAPP { get; set; }
    }

    public enum TipoHorario : int
    {
        [Display(Name = "Padrão")]
        Padrao = 1,
        [Display(Name = "Extra")]
        Extra = 2
    }

    public enum TipoAgendamento : int
    {
        [Display(Name = "Avulso")]
        Avulso = 1,
        [Display(Name = "Mensal")]
        Mensal = 2
    }
}
