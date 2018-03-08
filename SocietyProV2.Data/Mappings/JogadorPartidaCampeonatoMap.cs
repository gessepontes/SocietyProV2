using Dapper.FluentMap.Dommel.Mapping;
using SocietyProV2.Domain.Entities;

namespace SocietyProV2.Data.Mappings
{
    public class JogadorPartidaCampeonatoMap : DommelEntityMap<JogadorPartidaCampeonato>
    {
        public JogadorPartidaCampeonatoMap()
        {
            ToTable("JogadorPartidaCampeonato");
            Map(p => p.ID).IsKey().IsIdentity();
        }
    }
}
