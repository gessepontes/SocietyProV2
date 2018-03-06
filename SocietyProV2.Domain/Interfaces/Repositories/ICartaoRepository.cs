using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories.Common;
using System.Collections.Generic;

namespace SocietyProV2.Domain.Interfaces.Repositories
{
    public interface ICartaoRepository : IRepositoryBase<Cartao>
    {
        IEnumerable<Cartao> GetAllByCampeonato(int idCampeonato);
    }
}