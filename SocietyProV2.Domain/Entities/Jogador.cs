using SocietyProV2.Domain.Diversos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocietyProV2.Domain.Entities
{
    public class Jogador
    {
        public Jogador()
        {
            DATACADASTRO = DateTime.Now;
            STATUS = true;
            FOTO = "";
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

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nasc.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DATANASCIMENTO { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Time")]
        public int IDTIME { get; set; }

        [Display(Name = "Foto")]
        public string FOTO { get; set; }

        [Display(Name = "Telefone")]
        public string TELEFONE { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Rg")]
        public string RG { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Posição")]
        public Posicao POSICAO { get; set; }

        public bool STATUS { get; set; }

        [Display(Name = "Dispensado")]
        public bool DISPENSADO { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Data da Dispensa")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DATADISPENSA { get; set; }

        public DateTime DATACADASTRO { get; set; }

        public virtual Time Time { get; set; }
        public virtual ICollection<JogadorPartida> JogadorPartida { get; set; }

    }


    public class JogadorArtilharia
    {
        public int ID { get; set; }

        [Display(Name = "Gol")]
        public int Gol { get; set; }

        string sNome;
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

        string sNomeTime;
        [Display(Name = "Time")]
        public string NOMETIME
        {
            get
            {
                if (string.IsNullOrEmpty(sNomeTime))
                {
                    return sNomeTime;
                }
                return Diverso.FirstCharToUpper(sNomeTime);
            }
            set
            {
                sNomeTime = value;
            }

        }
    }
}
