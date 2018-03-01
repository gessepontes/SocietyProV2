using Microsoft.AspNetCore.Mvc.Rendering;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories.Common;
using System.Collections.Generic;

namespace SocietyProV2.Domain.Interfaces.Repositories
{
    public interface IInscricaoRepository : IRepositoryBase<Inscricao>
    {
        IEnumerable<Inscricao> GetAll(int idCampeoanto);
        SelectList GetDropAll(int idCampeoanto);
        SelectList GetDropAllGrupo(int idCampeoanto);
        SelectList GetDropEditGrupo(int id);
        void Ativar(int id);
    }
}