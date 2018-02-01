using Dapper.FluentMap.Dommel.Mapping;
using SocietyProV2.Domain.Entities;

namespace SocietyProV2.Data.Mappings
{
    public class JogadorInscritoMap : DommelEntityMap<JogadorInscrito>
    {
        public JogadorInscritoMap()
        {
            ToTable("JogadorInscrito");
            Map(p => p.ID).IsKey().IsIdentity();
        }
    }
}
