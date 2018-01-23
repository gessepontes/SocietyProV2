using Dapper.FluentMap.Dommel.Mapping;
using SocietyProV2.Domain.Entities;

namespace SocietyProV2.Data.Mappings
{
    public class CampoHorarioMap : DommelEntityMap<CampoHorario>
    {
        public CampoHorarioMap()
        {
            ToTable("CAMPOHORARIO");
            Map(p => p.ID).IsKey().IsIdentity();
        }
    }
}
