﻿using Dapper.FluentMap.Dommel.Mapping;
using SocietyProV2.Domain.Entities;

namespace SocietyProV2.Data.Mappings
{
    public class CampoValorMap : DommelEntityMap<CampoValor>
    {
        public CampoValorMap()
        {
            ToTable("CAMPOVALOR");
            Map(p => p.ID).IsKey().IsIdentity();
        }
    }
}
