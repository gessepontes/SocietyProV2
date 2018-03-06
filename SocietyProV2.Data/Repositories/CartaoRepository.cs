using Dapper;
using SocietyProV2.Data.Repositories.Common;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using System.Collections.Generic;

namespace SocietyProV2.Data.Repositories
{
    public class CartaoRepository : RepositoryBase<Cartao>, ICartaoRepository
    {
        public IEnumerable<Cartao> GetAllByCampeonato(int idCampeonato)
        {
            string sql = "";

            sql = "SELECT * FROM Cartao c INNER JOIN JogadorSumula js ON c.IDJogadorSumula = js.ID AND c.iTipoCartao <> 0 INNER JOIN JogadorInscrito ji ON ji.ID = js.IDJogadorInscrito  " +
                "INNER JOIN	 TIME t ON t.ID = ji.IDJogador INNER JOIN Sumula s ON S.ID = js.IDSumula INNER JOIN PartidaCampeonato pc ON pc.ID = s.IDPartidaCampeonato  " +
                "INNER JOIN Inscrito i ON i.ID = ji.IDInscrito AND i.IDCampeonato = @idCampeonato ";

            return conn.Query<Cartao>(sql, new { idCampeonato });
        }
    }
}
