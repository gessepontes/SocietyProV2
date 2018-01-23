using Dapper.FluentMap.Dommel.Mapping;
using SocietyProV2.Domain.Entities;

namespace SocietyProV2.Data.Mappings
{
    public class CampoItemMap : DommelEntityMap<CampoItem>
    {
        public CampoItemMap()
        {
            ToTable("CAMPOITEM");
            Map(p => p.ID).IsKey().IsIdentity();
        }
    }
}
