using System.Collections.Generic;
using Dapper;
using SocietyProV2.Data.Repositories.Common;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;

namespace SocietyProV2.Data.Repositories
{
    public class JogadorPartidaCampeonatoRepository : RepositoryBase<JogadorPartidaCampeonato>, IJogadorPartidaCampeonatoRepository
    {
        public IEnumerable<JogadorPartidaCampeonato> GetJogadorSumula(int idPartida, int idTipo)
        {

            string sql, parameter = "";

            if (idTipo == 1)
            {
                parameter = "INNER JOIN Inscrito i ON pc.IDInscrito1 = i.ID";
            }
            else
            {
                parameter = "INNER JOIN Inscrito i ON pc.IDInscrito2 = i.ID";
            }

            sql = @"SELECT jp.*,ji.*,j.* FROM PartidaCampeonato pc  " + parameter +
                " INNER JOIN JogadorInscrito ji ON ji.IDInscrito = i.ID INNER JOIN JOGADOR j ON ji.IDJogador = j.ID " +
                " LEFT JOIN  JogadorPartidaCampeonato jp ON pc.ID = jp.IDPartidaCampeonato AND ji.ID = jp.IDJogadorInscrito WHERE pc.ID = @idPartida";

            return conn.Query<JogadorPartidaCampeonato, JogadorInscrito, Jogador, JogadorPartidaCampeonato>(sql,
                    map: (jogadorPartidaCampeonato, jogadorInscrito, jogador) =>
                    {
                        if (jogadorPartidaCampeonato == null)
                        {
                            jogadorPartidaCampeonato = new JogadorPartidaCampeonato();
                        }

                        jogadorPartidaCampeonato.JogadorInscrito = jogadorInscrito;
                        jogadorPartidaCampeonato.JogadorInscrito.Jogador = jogador;
                        return jogadorPartidaCampeonato;
                    },
                            param: new { idPartida });
        }
    }
}
