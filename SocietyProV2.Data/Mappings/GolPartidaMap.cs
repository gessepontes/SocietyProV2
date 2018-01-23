using Dapper.FluentMap.Dommel.Mapping;
using SocietyProV2.Domain.Entities;

namespace SocietyProV2.Data.Mappings
{
    public class GolPartidaMap : DommelEntityMap<GolPartida>
    {
        public GolPartidaMap()
        {
            ToTable("GOL");
            Map(p => p.ID).IsKey().IsIdentity();
        }
    }
}
