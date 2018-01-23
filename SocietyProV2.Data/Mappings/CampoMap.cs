using Dapper.FluentMap.Dommel.Mapping;
using SocietyProV2.Domain.Entities;

namespace SocietyProV2.Data.Mappings
{
    public class CampoMap : DommelEntityMap<Campo>
    {
        public CampoMap()
        {
            ToTable("CAMPO");
            Map(p => p.ID).IsKey().IsIdentity();
        }
    }
}
