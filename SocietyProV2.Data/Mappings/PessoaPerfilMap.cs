using Dapper.FluentMap.Dommel.Mapping;
using SocietyProV2.Domain.Entities;

namespace SocietyProV2.Data.Mappings
{
    public class PessoaPerfilMap : DommelEntityMap<PessoaPerfil>
    {
        public PessoaPerfilMap()
        {
            ToTable("PESSOAPERFIL");
            Map(p => p.ID).IsKey().IsIdentity();
        }
    }
}
