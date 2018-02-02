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
    conn.Query<PreInscricao, Time, Pessoa, Inscricao, Inscricao>(
        @"SELECT * FROM PreInscrito PI INNER JOIN Time T ON PI.IDTime = T.ID INNER JOIN PESSOA P ON T.IDPESSOA = P.ID LEFT JOIN Inscrito I  ON PI.ID = I.IDPreInscrito WHERE PI.IDCampeonato = @idCampeoanto",
        map: (preInscricao, time, pessoa, inscricao) =>
        {
            if (inscricao == null) {
                inscricao = new Inscricao();
            }

            inscricao.PreInscricao = preInscricao;
            inscricao.PreInscricao.Time = time;
            inscricao.PreInscricao.Time.Pessoa = pessoa;
            return inscricao;
        },
                param: new { idCampeoanto });
    }
}
