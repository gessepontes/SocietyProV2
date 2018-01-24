using SocietyProV2.Domain.Diversos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocietyProV2.Domain.Entities
{
    public class Campeonato
    {
        public int IDCampeonato { get; set; }


        string sNomeCampeonato;
        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [StringLength(50)]
        [Display(Name = "Nome")]
        public string sNome
        {
            get
            {
                if (string.IsNullOrEmpty(sNomeCampeonato))
                {
                    return sNomeCampeonato;
                }
                return Diverso.FirstCharToUpper(sNomeCampeonato);
            }
            set
            {
                sNomeCampeonato = value;
            }

        }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data inicio")]
        public DateTime? dDataInicio { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data fim")]
        public DateTime? dDataFim { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Tipo de campeonato")]
        public TipoCampeonato iTipoCampeonato { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Tipo de arbitragem")]
        public TipoArbitragem iTipoArbitragem { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Times do campeonato")]
        public TipoTime TIPO { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Campo")]
        public int iCodCampo { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Quantidade de times")]
        public int iQuantidadeTimes { get; set; }

        [Display(Name = "Pré-Inscrição")]
        public bool bPreInscricao { get; set; }

        [Display(Name = "Ida e Volta")]
        public bool bIdaVolta { get; set; }

        [Display(Name = "Disponivel para transmissão")]
        public bool bDisponivelTransmissao { get; set; }

        [Display(Name = "Disponivel para inscrição de jogadores")]
        public bool bDisponivelInscricao { get; set; }

        public string LOGO { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Responsável")]
        public int IDPESSOA { get; set; }

        public DateTime? dDataCadastro { get; set; }

        public virtual Campo Campo { get; set; }

        public virtual ICollection<FotoInforCampeonato> FotoInforCampeonato { get; set; }
    }

    public enum TipoCampeonato : int
    {
        Grupos = 1,
        [Display(Name = "Mata-Mata")]
        MataMata = 2,
        [Display(Name = "Pontos Corridos")]
        PontosCorridos = 3
    }


    public enum TipoArbitragem : int
    {
        [Display(Name = "Society")]
        Society = 1,
        [Display(Name = "Campo de 11")]
        Campo = 2
    }
}
