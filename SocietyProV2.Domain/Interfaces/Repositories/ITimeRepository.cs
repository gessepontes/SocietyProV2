using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories.Common;
using System.Collections.Generic;

namespace SocietyProV2.Domain.Interfaces.Repositories
{
    public interface ITimeRepository : IRepositoryBase<Time> {
        IEnumerable<Time> GetAllTimeDrop();
    }
}