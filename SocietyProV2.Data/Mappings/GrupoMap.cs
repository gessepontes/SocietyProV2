using Dapper.FluentMap.Dommel.Mapping;
using SocietyProV2.Domain.Entities;

namespace SocietyProV2.Data.Mappings
{
    public class GrupoMap : DommelEntityMap<Grupo>
    {
        public GrupoMap()
        {
            ToTable("CampeonatoGrupo");
            Map(p => p.ID).IsKey().IsIdentity();
        }
    }
}
