using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories.Common;
using System.Collections.Generic;

namespace SocietyProV2.Domain.Interfaces.Repositories
{
    public interface IJogadorPartidaCampeonatoRepository : IRepositoryBase<JogadorPartidaCampeonato>
    {
        IEnumerable<JogadorPartidaCampeonato> GetJogadorSumula(int idPartida, int idTipo);
    }
}