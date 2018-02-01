using Dapper;
using SocietyProV2.Data.Repositories.Common;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using System.Collections.Generic;


namespace SocietyProV2.Data.Repositories
{
    public class InscricaoRepository : RepositoryBase<Inscricao>, IInscricaoRepository
    {
        public IEnumerable<Inscricao> GetAll(int idCampeoanto) =>
    conn.Query<Inscricao, PreInscricao, Time, Pessoa, Inscricao>(
        @"SELECT * FROM Inscrito I INNER JOIN PreInscrito PI ON PI.ID = I.IDPreInscrito  INNER JOIN Time T ON PI.IDTime = T.ID INNER JOIN PESSOA P ON T.IDPESSOA = P.ID WHERE PI.IDCampeonato = @idCampeoanto",
        map: (inscricao, preInscricao, time, pessoa) =>
        {
            inscricao.PreInscricao = preInscricao;
            inscricao.PreInscricao.Time = time;
            inscricao.PreInscricao.Time.Pessoa = pessoa;
            return inscricao;
        },
                param: new { idCampeoanto });
    }
}
