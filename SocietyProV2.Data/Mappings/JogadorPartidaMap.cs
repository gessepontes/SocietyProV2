using Dapper.FluentMap.Dommel.Mapping;
using SocietyProV2.Domain.Entities;

namespace SocietyProV2.Data.Mappings
{
    public class JogadorPartidaMap : DommelEntityMap<JogadorPartida>
    {
        public JogadorPartidaMap()
        {
            ToTable("JOGADORPARTIDA");
            Map(p => p.ID).IsKey().IsIdentity();
        }
    }
}
