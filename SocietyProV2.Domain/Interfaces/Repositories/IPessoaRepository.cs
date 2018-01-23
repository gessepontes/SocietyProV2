using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories.Common;
using System.Collections.Generic;

namespace SocietyProV2.Domain.Interfaces.Repositories
{
    public interface IPessoaRepository : IRepositoryBase<Pessoa> {

        void UpdateUser(Pessoa obj);
        IEnumerable<Pessoa> GetAllPessoaDrop();
        Pessoa GetByIdPessoaPerfil(int? id);
    }
}