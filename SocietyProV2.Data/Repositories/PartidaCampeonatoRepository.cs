using Dapper;
using SocietyProV2.Data.Repositories.Common;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using System.Collections.Generic;

namespace SocietyProV2.Data.Repositories
{
    public class PartidaCampeonatoRepository : RepositoryBase<PartidaCampeonato>, IPartidaCampeonatoRepository
    {
        public IEnumerable<PartidaCampeonato> GetAll(int id) =>
    conn.Query<PartidaCampeonato, Inscricao, Inscricao, PreInscricao, PreInscricao, Time, Time, PartidaCampeonato>(
        @"SELECT * FROM PartidaCampeonato pc INNER JOIN Inscrito i ON pc.IDInscrito1 = i.ID INNER JOIN Inscrito i2 ON pc.IDInscrito2 = i2.ID
INNER JOIN PreInscrito p ON p.ID = i.IDPreInscrito INNER JOIN PreInscrito p2 ON p2.ID = i2.IDPreInscrito
INNER JOIN TIME t ON t.ID = p.IDTime INNER JOIN TIME t2 ON t2.ID = p2.IDTime WHERE p.IDCampeonato = @id",
        map: (partidaCampeonato, inscricao, inscricao1, preInscricao, preInscricao1, time, time1) =>
        {
            partidaCampeonato.Inscricao = inscricao;
            partidaCampeonato.Inscricao.PreInscricao = preInscricao;
            partidaCampeonato.Inscricao.PreInscricao.Time = time;

            partidaCampeonato.Inscricao1 = inscricao1;
            partidaCampeonato.Inscricao1.PreInscricao = preInscricao1;
            partidaCampeonato.Inscricao1.PreInscricao.Time = time1;

            return partidaCampeonato;
        }, param: new { id });
    }
}
