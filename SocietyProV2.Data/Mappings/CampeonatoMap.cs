using Dapper.FluentMap.Dommel.Mapping;
using SocietyProV2.Domain.Entities;

namespace SocietyProV2.Data.Mappings
{
    public class CampeonatoMap : DommelEntityMap<Campeonato>
    {
        public CampeonatoMap()
        {
            ToTable("Campeonato");
            Map(p => p.IDCampeonato).IsKey().IsIdentity();
        }
    }
}
