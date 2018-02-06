using System.Collections.Generic;
using System.Linq;
using Dapper;
using SocietyProV2.Data.Repositories.Common;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;

namespace SocietyProV2.Data.Repositories
{
    public class JogadorInscritoRepository : RepositoryBase<JogadorInscrito>, IJogadorInscritoRepository
    {
        public IEnumerable<JogadorInscrito> GetAll(int idTime, int IDCampeonato) =>
            conn.Query<JogadorInscrito, Jogador, Inscricao, JogadorInscrito>(
                @"SELECT JI.*,J.*, I.* FROM JOGADOR J INNER JOIN (SELECT I.ID,C.dDataInicio,PI.IDTime FROM Inscrito I INNER JOIN PreInscrito PI ON I.IDPreInscrito = PI.ID INNER JOIN Campeonato C ON C.IDCampeonato = pi.IDCampeonato AND PI.IDCampeonato = @IDCampeonato AND PI.IDTime = @idTime) T ON T.IDTime = J.IDTIME INNER JOIN Inscrito I ON T.ID = I.ID LEFT JOIN JogadorInscrito JI ON J.ID = JI.IDJogador AND JI.IDInscrito = I.ID WHERE  J.STATUS = 1 AND (J.DATADISPENSA > T.dDataInicio OR  J.DATADISPENSA = '01/01/1900') AND J.DATACADASTRO <  T.dDataInicio",
                map: (jogadorInscrito, jogador, inscricao) =>
                {
                    if (jogadorInscrito == null)
                    {
                        jogadorInscrito = new JogadorInscrito();
                    }

                    jogadorInscrito.Inscricao = inscricao;
                    jogadorInscrito.Jogador = jogador;
                    return jogadorInscrito;
                },
                        param: new { idTime, IDCampeonato });


        public override JogadorInscrito GetById(int? id) =>
    conn.Query<JogadorInscrito, Inscricao, PreInscricao, JogadorInscrito>(
        @"SELECT TOP(1) * from JogadorInscrito JI INNER JOIN Inscrito I ON JI.IDInscrito = I.ID INNER JOIN PreInscrito PI ON I.IDPreInscrito = PI.ID WHERE JI.ID = @id",
        map: (jogadorInscrito, inscricao, preinscricao) =>
        {
            jogadorInscrito.Inscricao = inscricao;
            jogadorInscrito.Inscricao.PreInscricao = preinscricao;
            return jogadorInscrito;
        },
                param: new { id }).FirstOrDefault();


        public override void Add(JogadorInscrito obj)
        {
            JogadorInscrito ji = conn.Query<JogadorInscrito>("SELECT TOP(1) * FROM JogadorInscrito WHERE IDJogador = @IDJogador AND IDInscrito = @IDInscrito ", new { obj.IDJogador, obj.IDInscrito }).FirstOrDefault();

            if (ji == null)
            {
                conn.Execute("INSERT INTO JogadorInscrito (IDJogador,IDInscrito,STATUS) VALUES (@IDJogador,@IDInscrito,'A') ", new { obj.IDJogador, obj.IDInscrito });
            }
            else
            {
                conn.Execute("UPDATE JogadorInscrito SET STATUS='A',dDataDispensa = null WHERE ID = @id; ", new { id = ji.ID });
            }

        }

        public override void Remove(JogadorInscrito obj)
        {
            string sql = "";

            JogadorInscrito ji = conn.Query<JogadorInscrito>("SELECT TOP(1) * FROM JogadorSumula WHERE IDJogadorInscrito = @id ", new { obj.ID }).FirstOrDefault();

            if (ji == null)
            {
                sql = "DELETE FROM JogadorInscrito WHERE ID = @id; ";
            }
            else
            {
                sql = "UPDATE JogadorInscrito SET STATUS='D',dDataDispensa = getdate() WHERE ID = @id; ";
            }

            conn.Execute(sql, new { obj.ID });
        }

        public IEnumerable<JogadorInscrito> BidDetails(int idCampeonato) =>
    conn.Query<JogadorInscrito, Jogador, Inscricao, Time, JogadorInscrito>(
        @"SELECT JI.*,J.*,I.*,T.* FROM JogadorInscrito JI INNER JOIN JOGADOR J ON JI.IDJOGADOR = J.ID INNER JOIN Inscrito I ON JI.IDInscrito = I.ID INNER JOIN PreInscrito P ON P.ID = I.IDPreinscrito INNER JOIN TIME T ON J.idTime = T.ID WHERE P.IDCampeonato = @idCampeonato",
        map: (jogadorInscrito, jogador, inscricao,time) =>
        {
            jogadorInscrito.Inscricao = inscricao;
            jogadorInscrito.Jogador = jogador;
            jogadorInscrito.Jogador.Time = time;

            return jogadorInscrito;
        },
                param: new { idCampeonato });

    }
}
