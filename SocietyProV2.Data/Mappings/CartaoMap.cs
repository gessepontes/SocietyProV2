using Dapper.FluentMap.Dommel.Mapping;
using SocietyProV2.Domain.Entities;

namespace SocietyProV2.Data.Mappings
{
    public class CartaoMap : DommelEntityMap<Cartao>
    {
        public CartaoMap()
        {
            ToTable("Cartao");
            Map(p => p.ID).IsKey().IsIdentity();
        }
    }
}
