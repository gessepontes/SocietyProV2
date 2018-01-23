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

    public enum Posicao : int
    {
        [Display(Name = "Goleiro")]
        Goleiro = 0,
        [Display(Name = "Zagueiro")]
        Zagueiro = 1,
        [Display(Name = "Volante")]
        Volante = 2,
        [Display(Name = "Meio-Campo")]
        MeioCampo = 3,
        [Display(Name = "Atacante")]
        Atacante = 4,
        [Display(Name = "Chupa-Sangue")]
        ChupaSangue = 5,
        [Display(Name = "Matador")]
        Matador = 6,
        [Display(Name = "Lateral")]
        Lateral = 7
    }
}
