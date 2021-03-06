﻿using SocietyProV2.Domain.Diversos;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories.Common;
using System.Collections.Generic;

namespace SocietyProV2.Domain.Interfaces.Repositories
{
    public interface ICampeonatoRepository : IRepositoryBase<Campeonato>
    {
        IEnumerable<Campeonato> GetGrupoAll();
        IEnumerable<TimeClassificacao> Classificacao(int idCampeonato, IDGrupo? IDGrupo);
        IEnumerable<JogadorArtilharia> Artilharia(int idCampeonato);
    }
}