using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories.Common;
using System.Collections.Generic;

namespace SocietyProV2.Domain.Interfaces.Repositories
{
    public interface IGolPartidaRepository : IRepositoryBase<GolPartida>
    {
        IEnumerable<GolPartida> GetAllPartida(int idPartida,int idTime);
        int Add(GolPartida obj, int idPartida, int idTime);

    }
}