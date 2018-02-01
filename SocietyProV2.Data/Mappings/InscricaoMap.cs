using Dapper.FluentMap.Dommel.Mapping;
using SocietyProV2.Domain.Entities;

namespace SocietyProV2.Data.Mappings
{
    public class InscricaoMap : DommelEntityMap<Inscricao>
    {
        public InscricaoMap()
        {
            ToTable("Inscrito");
            Map(p => p.ID).IsKey().IsIdentity();
        }
    }
}
