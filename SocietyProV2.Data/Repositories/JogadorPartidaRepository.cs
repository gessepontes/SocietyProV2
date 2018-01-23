using System.Collections.Generic;
using Dapper;
using SocietyProV2.Data.Repositories.Common;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;

namespace SocietyProV2.Data.Repositories
{
    public class JogadorPartidaRepository : RepositoryBase<JogadorPartida>, IJogadorPartidaRepository
    {
        public IEnumerable<JogadorPartida> GetAll(int IDTime, int IDPartida) =>
            conn.Query<JogadorPartida, Jogador, Partida, JogadorPartida>(
                @"SELECT * from JOGADORPARTIDA JP RIGHT JOIN JOGADOR J ON JP.IDJOGADOR = J.ID AND JP.IDPARTIDA = @IDPartida LEFT JOIN PARTIDA P ON P.ID = JP.IDPARTIDA WHERE J.IDTIME = @IDTime AND J.STATUS = 1 AND (J.DATADISPENSA > (SELECT DATAPARTIDA FROM PARTIDA WHERE ID=@IDPartida) OR  J.DATADISPENSA = '01/01/1900') AND J.DATACADASTRO < (SELECT DATAPARTIDA FROM PARTIDA WHERE ID=@IDPartida)",
                map: (jogadorPartida, jogador, partida) =>
                {
                    jogadorPartida.Partida = partida;
                    jogadorPartida.Jogador = jogador;
                    return jogadorPartida;
                },
                        param: new { IDTime, IDPartida });
    }
}
