using Dapper;
using SocietyProV2.Data.Repositories.Common;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using System.Collections.Generic;


namespace SocietyProV2.Data.Repositories
{
    public class PreInscricaoRepository : RepositoryBase<PreInscricao>, IPreInscricaoRepository
    {
        public IEnumerable<PreInscricao> GetAll(int idCampeoanto) =>
    conn.Query<PreInscricao, Time, Pessoa, PreInscricao>(
        @"SELECT * FROM PreInscrito PI INNER JOIN Time T ON PI.IDTime = T.ID INNER JOIN PESSOA P ON T.IDPESSOA = P.ID WHERE PI.IDCampeonato = @idCampeoanto",
        map: (preInscricao, time, pessoa) =>
        {
            preInscricao.Time = time;
            preInscricao.Time.Pessoa = pessoa;
            return preInscricao;
        },
                param: new { idCampeoanto });
    }
}
