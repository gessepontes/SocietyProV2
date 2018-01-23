using Dapper.FluentMap.Dommel.Mapping;
using SocietyProV2.Domain.Entities;

namespace SocietyProV2.Data.Mappings
{
    public class CidadeMap : DommelEntityMap<Cidade>
    {
        public CidadeMap()
        {
            ToTable("Cidade");
            Map(p => p.ID).IsKey().IsIdentity();
        }
    }
}
