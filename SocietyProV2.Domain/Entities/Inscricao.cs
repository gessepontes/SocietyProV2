using System.Collections.Generic;

namespace SocietyProV2.Domain.Entities
{
    public class Inscricao
    {
        public int ID { get; set; }

        public int IDPreInscrito { get; set; }

        public virtual PreInscricao PreInscricao { get; set; }

        public virtual ICollection<Grupo> Grupo { get; set; }
    }
}
