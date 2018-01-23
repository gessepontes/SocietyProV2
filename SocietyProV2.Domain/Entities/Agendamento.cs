using SocietyProV2.Domain.Diversos;
using System;
using System.ComponentModel.DataAnnotations;

namespace SocietyProV2.Domain.Entities
{
    public class Agendamento
    {
        public int ID { get; set; }

        [Display(Name = "Campo")]
        public string CAMPO { get; set; }

        [Display(Name = "Data")]
        public DateTime DATA { get; set; }

        [Display(Name = "Hora")]
        public string HORA { get; set; }

        [Display(Name = "Status")]
        public string STATUS { get; set; }

        string sNome;
        [Display(Name = "Cliente")]
        public string PESSOA
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

        [Display(Name = "Telefone")]
        public string TELEFONE { get; set; }
    }
}
