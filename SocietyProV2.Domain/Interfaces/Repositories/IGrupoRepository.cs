using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories.Common;
using System.Collections.Generic;

namespace SocietyProV2.Domain.Interfaces.Repositories
{
    public interface IGrupoRepository : IRepositoryBase<Grupo> {
        IEnumerable<Grupo> GetAllByCampeonato(int id);
        int CreateAutomatico(int iQuantidadeTimes, int IDCampeonato);
        Grupo GetByIdInscricao(int id);
    }

}