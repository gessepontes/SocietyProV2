using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories.Common;
using System.Collections.Generic;

namespace SocietyProV2.Domain.Interfaces.Repositories
{
    public interface IAgendarRepository : IRepositoryBase<Agendar>
    {
        IEnumerable<Agendamento> GetAllAgendamento();
        Agendamento GetByIdAgendamento(int? id);
        void Status(int id,char status);
    }
}