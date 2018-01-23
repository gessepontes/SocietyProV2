using SocietyProV2.Domain.Diversos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocietyProV2.Domain.Entities
{
    public class Campo
    {
        public Campo()
        {
            DATACADASTRO = DateTime.Now;
            STATUS = true;
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

        string sEndereco;
        [StringLength(200)]
        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Endereço")]
        public string ENDERECO
        {
            get
            {
                if (string.IsNullOrEmpty(sEndereco))
                {
                    return sEndereco;
                }
                return Diverso.FirstCharToUpper(sEndereco);
            }
            set
            {
                sEndereco = value;
            }
        }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [StringLength(15)]
        [Display(Name = "Telefone")]
        public string TELEFONE { get; set; }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Column(TypeName = "money")]
        public decimal VALOR { get; set; }

        [Display(Name = "Valor Mensal")]
        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Column(TypeName = "money")]
        public decimal VALORMENSAL { get; set; }

        public bool STATUS { get; set; }

        [Display(Name = "Society")]
        public bool SOCIETY { get; set; }

        [Display(Name = "Campo de 11")]
        public bool CAMPO11 { get; set; }

        [Display(Name = "Disponível agendamento")]
        public bool AGENDAMENTO { get; set; }

        [Display(Name = "Logo do Campo")]
        public string LOGO { get; set; }

        public DateTime DATACADASTRO { get; set; }

        [Display(Name = "Administrador do Campo")]
        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        public int IDPESSOA { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        public int IDCIDADE { get; set; }

        [Display(Name = "Administrador do Campo")]
        public virtual Pessoa Pessoa { get; set; }

        [Display(Name = "Cidade")]
        public virtual Cidade Cidade { get; set; }

        [Display(Name = "Campo Item")]
        public virtual ICollection<CampoItem> CampoItem { get; set; }

        [Display(Name = "Horário")]
        public virtual ICollection<CampoHorario> CampoHorario { get; set; }

    }
}
