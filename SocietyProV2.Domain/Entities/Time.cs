using SocietyProV2.Domain.Diversos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocietyProV2.Domain.Entities
{
    public class Time
    {
        public Time()
        {
            DATACADASTRO = DateTime.Now;
            STATUS = true;
            ATIVO = true;
            FOTO = "";
            SIMBOLO = "";
        }

        public int ID { get; set; }


        string sNome;
        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [StringLength(100)]
        [Display(Name = "Nome")]
        public string NOME
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

        [Display(Name = "Pessoa")]
        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        public string IDPESSOA { get; set; }

        [Display(Name = "Simbolo")]
        public string SIMBOLO { get; set; }

        [Display(Name = "Observação")]
        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        public string OBSERVACAO { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de fundação")]
        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        public DateTime DATAFUNDACAO { get; set; }

        [Display(Name = "Ativo")]
        public bool ATIVO { get; set; }

        [Display(Name = "Status")]
        public bool STATUS { get; set; }

        public DateTime DATACADASTRO { get; set; }

        [Display(Name = "Foto")]
        public string FOTO { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        public TipoTime TIPO { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        [Display(Name = "Jogador")]
        public virtual ICollection<Jogador> Jogador { get; set; }

        [Display(Name = "Horário")]
        public virtual ICollection<CampoHorario> CampoHorario { get; set; }

        [Display(Name = "Pré-Inscrição")]
        public virtual ICollection<PreInscricao> PreInscricao { get; set; }

    }

    public enum TipoTime : int
    {
        [Display(Name = "Society")]
        Society = 0,
        [Display(Name = "Campo de 11")]
        Campo = 1
    }

}
