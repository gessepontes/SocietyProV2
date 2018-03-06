using SocietyProV2.Domain.Diversos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocietyProV2.Domain.Entities
{
    public class Partida
    {
        public Partida()
        {
            DATACADASTRO = DateTime.Now;
            STATUS = true;
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Time 1")]
        public int IDTIME1 { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Time 2")]
        public int IDTIME2 { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Partida")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DATAPARTIDA { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Hora da Partida")]
        [StringLength(10)]
        public string HORAPARTIDA { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Campo")]
        public int IDCAMPO { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Gol 1")]
        [Range(0, 30, ErrorMessage = "O quantidade de gols deve estar entre 0 e 30.")]
        public int GOL1 { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Gol 2")]
        [Range(0, 30, ErrorMessage = "O quantidade de gols deve estar entre 0 e 30.")]
        public int GOL2 { get; set; }

        string sNome;
        [Display(Name = "Time não cadastrado")]
        [StringLength(50)]
        public string TIMENAOCADASTRADO
        {
            get
            {
                if (string.IsNullOrEmpty(sNome))
                {
                    return sNome;
                }
                return Diverso.FirstCharToUpper(sNome);
            }
            set
            {
                sNome = value;
            }

        }

        [Display(Name = "Status")]
        public StatusP STATUSPARTIDA { get; set; }

        public bool STATUS { get; set; }

        public DateTime DATACADASTRO { get; set; }

        public virtual Campo Campo { get; set; }

        [Display(Name = "Casa")]
        public virtual Time TimeCasa { get; set; }

        [Display(Name = "Fora")]
        public virtual Time TimeFora { get; set; }

        public virtual ICollection<JogadorPartida> JogadorPartida { get; set; }

    }
}
