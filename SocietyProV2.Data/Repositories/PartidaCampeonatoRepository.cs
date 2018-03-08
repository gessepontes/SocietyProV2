using Dapper;
using SocietyProV2.Data.Repositories.Common;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SocietyProV2.Data.Repositories
{
    public class PartidaCampeonatoRepository : RepositoryBase<PartidaCampeonato>, IPartidaCampeonatoRepository
    {
        public IEnumerable<PartidaCampeonato> GetAll(int id) =>
    conn.Query<PartidaCampeonato, Inscricao, Inscricao, Time, Time, PartidaCampeonato>(
        @"SELECT * FROM PartidaCampeonato pc INNER JOIN Inscrito i ON pc.IDInscrito1 = i.ID INNER JOIN Inscrito i2 ON pc.IDInscrito2 = i2.ID
INNER JOIN TIME t ON t.ID = i.IDTime INNER JOIN TIME t2 ON t2.ID = i2.IDTime WHERE i.IDCampeonato = @id",
        map: (partidaCampeonato, inscricao, inscricao1, time, time1) =>
        {
            partidaCampeonato.Inscricao = inscricao;
            partidaCampeonato.Inscricao.Time = time;

            partidaCampeonato.Inscricao1 = inscricao1;
            partidaCampeonato.Inscricao1.Time = time1;

            return partidaCampeonato;
        }, param: new { id });


        public override PartidaCampeonato GetById(int? id) =>
    conn.Query<PartidaCampeonato, Inscricao, Inscricao, Time, Time, PartidaCampeonato>(
        @"SELECT TOP(1) * FROM PartidaCampeonato pc INNER JOIN Inscrito i ON pc.IDInscrito1 = i.ID INNER JOIN Inscrito i2 ON pc.IDInscrito2 = i2.ID
INNER JOIN TIME t ON t.ID = i.IDTime INNER JOIN TIME t2 ON t2.ID = i2.IDTime WHERE pc.ID = @id",
        map: (partidaCampeonato, inscricao, inscricao1, time, time1) =>
        {
            partidaCampeonato.Inscricao = inscricao;
            partidaCampeonato.Inscricao.Time = time;

            partidaCampeonato.Inscricao1 = inscricao1;
            partidaCampeonato.Inscricao1.Time = time1;

            return partidaCampeonato;
        }, param: new { id }).FirstOrDefault();


        public PartidaCampeonato GetSumulaById(int id) =>
            conn.Query<PartidaCampeonato, Inscricao, Inscricao, Time, Time,Campo,CampoItem, PartidaCampeonato>(
            @"SELECT TOP(1) * FROM PartidaCampeonato pc INNER JOIN Inscrito i ON pc.IDInscrito1 = i.ID INNER JOIN Inscrito i2 ON pc.IDInscrito2 = i2.ID
            INNER JOIN TIME t ON t.ID = i.IDTime INNER JOIN TIME t2 ON t2.ID = i2.IDTime INNER JOIN CAMPO c ON c.ID = pc.IDCAMPO LEFT JOIN CAMPOITEM cp ON pc.IDCAMPOITEM = cp.ID WHERE pc.ID = @id",
            map: (partidaCampeonato, inscricao, inscricao1, time, time1,campo,campoItem) =>
            {
                partidaCampeonato.Inscricao = inscricao;
                partidaCampeonato.Inscricao.Time = time;

                partidaCampeonato.Inscricao1 = inscricao1;
                partidaCampeonato.Inscricao1.Time = time1;
                partidaCampeonato.Campo = campo;
                partidaCampeonato.CampoItem = campoItem;

                return partidaCampeonato;
            }, param: new { id }).FirstOrDefault();

            public int CreateAutomatico(int IDCampeonato)
            {
                return 1;
            }

    }
}
