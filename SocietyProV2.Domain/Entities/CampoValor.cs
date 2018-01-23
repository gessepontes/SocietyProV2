using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocietyProV2.Domain.Entities
{
    public class CampoValor
    {
        public CampoValor()
        {
            DATAINICIO = DateTime.Now;
        }

        public int ID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data Inicio")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DATAINICIO { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data Fim")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DATAFIM { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Display(Name = "Campo")]
        public int IDCAMPOITEM { get; set; }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Column(TypeName = "money")]
        public decimal VALOR { get; set; }

        [Display(Name = "Valor Mensal")]
        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Column(TypeName = "money")]
        public decimal VALORMENSAL { get; set; }

        [Display(Name = "Campo")]
        public virtual CampoItem CampoItem { get; set; }

    }
}
