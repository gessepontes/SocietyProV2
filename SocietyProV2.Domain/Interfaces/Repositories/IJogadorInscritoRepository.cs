using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories.Common;
using System.Collections.Generic;

namespace SocietyProV2.Domain.Interfaces.Repositories
{
    public interface IJogadorInscritoRepository : IRepositoryBase<JogadorInscrito> {
        IEnumerable<JogadorInscrito> GetAll(int idTime, int IDCampeonato);
        IEnumerable<JogadorInscrito> BidDetails(int idCampeonato);
    }
}