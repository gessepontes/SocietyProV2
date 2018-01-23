using Dapper.FluentMap.Dommel.Mapping;
using SocietyProV2.Domain.Entities;

namespace SocietyProV2.Data.Mappings
{
    public class AgendarMap : DommelEntityMap<Agendar>
    {
        public AgendarMap()
        {
            ToTable("HORARIOAGENDADO");
            Map(p => p.ID).IsKey().IsIdentity();
        }
    }
}
