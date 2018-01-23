using Dapper;
using SocietyProV2.Data.Repositories.Common;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SocietyProV2.Data.Repositories
{
    public class PartidaRepository : RepositoryBase<Partida>, IPartidaRepository
    {
        public override IEnumerable<Partida> GetAll() =>
            conn.Query<Partida, Time, Time, Partida>(
                @"SELECT * FROM PARTIDA P INNER JOIN TIME T ON T.ID = P.IDTIME1 INNER JOIN TIME T2 ON T2.ID = P.IDTIME2",
                map: (partida, time, timefora) =>
                {
                    partida.TimeCasa = time;
                    partida.TimeFora = timefora;
                    return partida;
                });

        public IEnumerable<Partida> GetAllAutorizar() =>
    conn.Query<Partida, Time, Time, Partida>(
        @"SELECT * FROM PARTIDA P INNER JOIN TIME T ON T.ID = P.IDTIME1 INNER JOIN TIME T2 ON T2.ID = P.IDTIME2 WHERE STATUSPARTIDA = 0",
        map: (partida, time, timefora) =>
        {
            partida.TimeCasa = time;
            partida.TimeFora = timefora;
            return partida;
        });

        public override Partida GetById(int? id) =>
    conn.Query<Partida, Time, Time, Campo, Partida>(
        @"SELECT TOP(1) * FROM PARTIDA P INNER JOIN TIME T ON T.ID = P.IDTIME1 INNER JOIN TIME T2 ON T2.ID = P.IDTIME2 INNER JOIN CAMPO C ON C.ID = P.IDCAMPO WHERE P.ID = @partidaID",
        map: (partida, time, timefora, campo) =>
        {
            partida.TimeCasa = time;
            partida.TimeFora = timefora;
            partida.Campo = campo;
            return partida;
        },
        param: new { partidaID = id }).FirstOrDefault();

        public void Aprovar(int id) => conn.Execute("UPDATE PARTIDA SET STATUSPARTIDA=1 WHERE ID = @id; ", new { id });

        public void Cancelar(int id) => conn.Execute("UPDATE PARTIDA SET STATUSPARTIDA=2 WHERE ID = @id; ", new { id });
    }
}
