﻿using Dapper.FluentMap.Dommel.Mapping;
using SocietyProV2.Domain.Entities;

namespace SocietyProV2.Data.Mappings
{
    public class HorarioMap : DommelEntityMap<Horario>
    {
        public HorarioMap()
        {
            ToTable("HORARIOPADRAO");
            Map(p => p.ID).IsKey().IsIdentity();
        }
    }
}
