using Dapper.FluentMap.Dommel.Mapping;
using SocietyProV2.Domain.Entities;

namespace SocietyProV2.Data.Mappings
{
    public class FotoInforCampeonatoMap : DommelEntityMap<FotoInforCampeonato>
    {
        public FotoInforCampeonatoMap()
        {
            ToTable("FOTOINFORCAMPEONATO");
            Map(p => p.ID).IsKey().IsIdentity();
        }
    }
}
