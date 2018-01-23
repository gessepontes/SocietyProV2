using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories.Common;
using System.Collections.Generic;

namespace SocietyProV2.Domain.Interfaces.Repositories
{
    public interface IPartidaRepository : IRepositoryBase<Partida> {
        IEnumerable<Partida> GetAllAutorizar();
        void Aprovar(int id);
        void Cancelar(int id);
    }
}