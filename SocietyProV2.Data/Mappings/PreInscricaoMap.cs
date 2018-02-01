using Dapper.FluentMap.Dommel.Mapping;
using SocietyProV2.Domain.Entities;

namespace SocietyProV2.Data.Mappings
{
    public class PreInscricaoMap : DommelEntityMap<PreInscricao>
    {
        public PreInscricaoMap()
        {
            ToTable("PreInscrito");
            Map(p => p.ID).IsKey().IsIdentity();
        }
    }
}
