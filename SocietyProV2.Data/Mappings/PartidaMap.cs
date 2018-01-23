using Dapper.FluentMap.Dommel.Mapping;
using SocietyProV2.Domain.Entities;

namespace SocietyProV2.Data.Mappings
{
    public class PartidaMap : DommelEntityMap<Partida>
    {
        public PartidaMap()
        {
            ToTable("PARTIDA");
            Map(p => p.ID).IsKey().IsIdentity();
        }
    }
}
