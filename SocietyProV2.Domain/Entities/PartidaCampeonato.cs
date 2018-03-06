using System;
using System.ComponentModel.DataAnnotations;

namespace SocietyProV2.Domain.Entities
{
    public class PartidaCampeonato
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Time 1")]
        public int IDInscrito1 { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Time 2")]
        public int IDInscrito2 { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Gol time 1")]
        public int? iQntGols1 { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Gol time 2")]
        public int? iQntGols2 { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Rodada")]
        public string iRodada { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data da Partida")]
        public DateTime dDataPartida { get; set; }

        [StringLength(10)]
        [Display(Name = "Hora da Partida")]
        public string sHoraPartida { get; set; }

        public DateTime? dDataCadastro { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Campo")]
        public int IDCAMPO { get; set; }

        [Display(Name = "Campo Item")]
        public int? IDCAMPOITEM { get; set; }

        [Display(Name = "Liberar para classificação")]
        public Boolean CLASSIFICACAO { get; set; }

        [Display(Name = "Observação")]
        public String sObservacao { get; set; }

        public virtual Inscricao Inscricao { get; set; }

        public virtual Inscricao Inscricao1 { get; set; }

        public virtual Campo Campo { get; set; }
        public virtual CampoItem CampoItem { get; set; }
    }
}
