using Dapper;
using SocietyProV2.Data.Repositories.Common;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocietyProV2.Data.Repositories
{
    public class GolPartidaRepository : RepositoryBase<GolPartida>, IGolPartidaRepository
    {
        public IEnumerable<GolPartida> GetAllPartida(int idPartida, int idTime) =>
    conn.Query<GolPartida, JogadorPartida, Jogador, GolPartida>(
        @"SELECT * FROM GOL G RIGHT JOIN JOGADORPARTIDA JP ON JP.ID = G.IDJOGADORPARTIDA INNER JOIN JOGADOR J ON J.ID = JP.IDJOGADOR AND J.IDTIME=@idTime WHERE JP.IDPARTIDA = @idPartida order by J.NOME",
        map: (golPartida, jogadorPartida, jogador) =>
        {
            golPartida.JogadorPartida = jogadorPartida;
            golPartida.JogadorPartida.Jogador = jogador;
            return golPartida;
        },
                param: new { idPartida, idTime });


        public int Add(GolPartida obj, int idPartida, int idTime)
        {
            string sql;
            int iQntGeral;

            sql = "SELECT * FROM PARTIDA WHERE ID = @idPartida";

            Partida partida = conn.Query<Partida>(sql, new { idPartida }).SingleOrDefault();

            if (partida.IDTIME1 == idTime)
            {
                iQntGeral = partida.GOL1;
            }
            else
            {
                iQntGeral = partida.GOL2;
            }

            sql = "SELECT ISNULL(SUM(G.QTDGOL),0) FROM GOL G INNER JOIN JOGADORPARTIDA JP ON JP.ID = G.IDJOGADORPARTIDA AND JP.IDJOGADOR IN (SELECT ID FROM JOGADOR WHERE IDTIME = @idTime) ";
            sql = sql + "INNER JOIN JOGADOR J ON J.ID = JP.IDJOGADOR  WHERE JP.IDPARTIDA = @idPartida AND J.IDTIME = @idTime";

            int result = conn.Query<int>(sql, new { idPartida, idTime }).SingleOrDefault();

            switch (obj.QTDGOL)
            {
                case 0:
                    return 0;
                default:
                    if (obj.QTDGOL + result <= iQntGeral)
                    {
                        sql = "INSERT INTO GOL(IDJOGADORPARTIDA,QTDGOL,STATUS,DATACADASTRO) ";
                        sql = sql + "values(@IDJOGADORPARTIDA,@QTDGOL,@STATUS,@DATACADASTRO)";

                        conn.Query(sql, new { obj.IDJOGADORPARTIDA, obj.QTDGOL, obj.STATUS, obj.DATACADASTRO });

                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
            }
        }
    }
}
