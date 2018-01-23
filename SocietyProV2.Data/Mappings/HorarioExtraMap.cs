using Dapper.FluentMap.Dommel.Mapping;
using SocietyProV2.Domain.Entities;

namespace SocietyProV2.Data.Mappings
{
    public class HorarioExtraMap : DommelEntityMap<HorarioExtra>
    {
        public HorarioExtraMap()
        {
            ToTable("HORARIOEXTRA");
            Map(p => p.ID).IsKey().IsIdentity();
        }
    }
}
