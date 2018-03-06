using Dapper;
using SocietyProV2.Data.Repositories.Common;
using SocietyProV2.Domain.Diversos;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SocietyProV2.Data.Repositories
{
    public class CampeonatoRepository : RepositoryBase<Campeonato>, ICampeonatoRepository
    {
        public override IEnumerable<Campeonato> GetAll() =>
    conn.Query<Campeonato, Campo, Campeonato>(
        @"SELECT * FROM CAMPEONATO C INNER JOIN CAMPO CP ON C.icodcampo = CP.ID",
        map: (campeonato, campo) =>
        {
            campeonato.Campo = campo;
            return campeonato;
        });

        public IEnumerable<Campeonato> GetGrupoAll() =>
            conn.Query<Campeonato, Campo, Campeonato>(
            @"SELECT * FROM CAMPEONATO C INNER JOIN CAMPO CP ON C.icodcampo = CP.ID WHERE C.iTipoCampeonato = 1",
            map: (campeonato, campo) =>
            {
                campeonato.Campo = campo;
                return campeonato;
            });


        public override Campeonato GetById(int? id) {
            Campeonato result = conn.Query<Campeonato>("SELECT * FROM CAMPEONATO C WHERE C.IDCampeonato = @id", new { id }).FirstOrDefault();
            ICollection<FotoInforCampeonato> resultInfor = conn.Query<FotoInforCampeonato>("SELECT * FROM FOTOINFORCAMPEONATO FC WHERE FC.IDCAMPEONATO = @id", new { id }).ToList();

            result.FotoInforCampeonato = resultInfor;
            return result;

        }

        public IEnumerable<TimeClassificacao> Classificacao(int idCampeonato, IDGrupo? IDGrupo)
        {
            string sql = "";
            string parametros = "";

            if (IDGrupo != null) parametros = parametros + "INNER JOIN CampeonatoGrupo cg ON cg.IDInscrito = i.id AND cg.IDGrupo = @IDGrupo";

            sql = "SELECT i.ID,t.NOME,((select ISNULL(SUM(CASE WHEN pc.iQntGols1 > pc.iQntGols2 THEN 3 WHEN pc.iQntGols1 = pc.iQntGols2 THEN 1 ELSE 0 END),0) " +
                "FROM PartidaCampeonato pc WHERE IDInscrito1 = i.ID AND pc.CLASSIFICACAO = 1) +(select ISNULL(SUM(CASE WHEN pc.iQntGols1 < pc.iQntGols2 THEN 3 WHEN pc.iQntGols1 = pc.iQntGols2 THEN 1 ELSE 0 END), 0) " +
                "FROM PartidaCampeonato pc WHERE IDInscrito2 = i.ID AND pc.CLASSIFICACAO = 1)) Pontuacao, " +
                "((select ISNULL(SUM(CASE WHEN pc.iQntGols1 > pc.iQntGols2 THEN 1 ELSE 0 END), 0) " +
                "FROM PartidaCampeonato pc WHERE IDInscrito1 = i.ID AND pc.CLASSIFICACAO = 1) +(select ISNULL(SUM(CASE WHEN pc.iQntGols1 < pc.iQntGols2 THEN 1 ELSE 0 END), 0) " +
                "FROM PartidaCampeonato pc WHERE IDInscrito2 = i.ID AND pc.CLASSIFICACAO = 1)) Vitoria,((select ISNULL(SUM(CASE WHEN pc.iQntGols1 < pc.iQntGols2 THEN 1 ELSE 0 END), 0) " +
                "FROM PartidaCampeonato pc WHERE IDInscrito1 = i.ID AND pc.CLASSIFICACAO = 1) +(select ISNULL(SUM(CASE WHEN pc.iQntGols1 > pc.iQntGols2 THEN 1 ELSE 0 END), 0) " +
                "FROM PartidaCampeonato pc WHERE IDInscrito2 = i.ID AND pc.CLASSIFICACAO = 1)) Derrota, " +
                "((select ISNULL(SUM(CASE WHEN pc.iQntGols1 = pc.iQntGols2 THEN 1 ELSE 0 END), 0) " +
                "FROM PartidaCampeonato pc WHERE IDInscrito1 = i.ID AND pc.CLASSIFICACAO = 1) +(select ISNULL(SUM(CASE WHEN pc.iQntGols1 = pc.iQntGols2 THEN 1 ELSE 0 END), 0) " +
                "FROM PartidaCampeonato pc WHERE IDInscrito2 = i.ID AND pc.CLASSIFICACAO = 1)) Empate, ((select ISNULL(SUM(pc.iQntGols1), 0) " +
                "FROM PartidaCampeonato pc WHERE IDInscrito1 = i.ID AND pc.CLASSIFICACAO = 1) +(select ISNULL(SUM(pc.iQntGols2), 0) FROM PartidaCampeonato pc WHERE IDInscrito2 = i.ID AND pc.CLASSIFICACAO = 1)) GolP," +
                "((select ISNULL(SUM(pc.iQntGols2), 0) FROM PartidaCampeonato pc WHERE IDInscrito1 = i.ID AND pc.CLASSIFICACAO = 1) +(select ISNULL(SUM(pc.iQntGols1), 0) FROM PartidaCampeonato pc WHERE IDInscrito2 = i.ID AND pc.CLASSIFICACAO = 1)) GolC," +
                "(((select ISNULL(SUM(pc.iQntGols1), 0) FROM PartidaCampeonato pc WHERE IDInscrito1 = i.ID AND pc.CLASSIFICACAO = 1) +(select ISNULL(SUM(pc.iQntGols2), 0) " +
                "FROM PartidaCampeonato pc WHERE IDInscrito2 = i.ID AND pc.CLASSIFICACAO = 1)) -((select ISNULL(SUM(pc.iQntGols2), 0) FROM PartidaCampeonato pc WHERE IDInscrito1 = i.ID AND pc.CLASSIFICACAO = 1) +(select ISNULL(SUM(pc.iQntGols1), 0) " +
                "FROM PartidaCampeonato pc WHERE IDInscrito2 = i.ID AND pc.CLASSIFICACAO = 1))) Saldo FROM Inscrito i INNER JOIN TIME t ON t.ID = i.IDTime "  + parametros + " WHERE i.IDCampeonato = @idCampeonato ORDER BY Pontuacao DESC, Vitoria DESC,Saldo DESC, GolP  DESC";

            return conn.Query<TimeClassificacao>(sql, new { idCampeonato, IDGrupo });
        }

        public IEnumerable<JogadorArtilharia> Artilharia(int idCampeonato)
        {
            string sql = "";

            sql = "SELECT j.ID,j.NOME,t.NOME NOMETIME,SUM(gc.iQuantidade) Gol FROM golCampeonato gc INNER JOIN JogadorSumula jc ON gc.iCodJogadorSumula = jc.id  " +
                "INNER JOIN JogadorInscrito ji ON jc.IDJogadorInscrito = ji.ID INNER JOIN Inscrito i ON i.ID = ji.IDInscrito AND i.IDCampeonato = @idCampeonato  " +
                "INNER JOIN TIME t ON i.IDTime = t.ID INNER JOIN JOGADOR j ON j.ID = ji.IDJogador GROUP BY j.ID,j.NOME,t.NOME " +
                "HAVING SUM(gc.iQuantidade) > 0 ORDER BY SUM(gc.iQuantidade) DESC";

            return conn.Query<JogadorArtilharia>(sql, new { idCampeonato });
        }
    }
}
