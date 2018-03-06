using SocietyProV2.Domain.Diversos;

namespace SocietyProV2.Domain.Entities
{
    public class PessoaPerfil
    {
        public int ID { get; set; }
        public TipoPerfil TIPOPERFIL { get; set; }
        public int IDPESSOA { get; set; }
        public virtual Pessoa PESSOA { get; set; }

    }
}
