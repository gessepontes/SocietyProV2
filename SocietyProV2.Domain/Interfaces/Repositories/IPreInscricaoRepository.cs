using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories.Common;
using System;
using System.Collections.Generic;

namespace SocietyProV2.Domain.Interfaces.Repositories
{
    public interface IPreInscricaoRepository : IRepositoryBase<PreInscricao>
    {
        IEnumerable<PreInscricao> GetAll(int idCampeoanto);
    }
}