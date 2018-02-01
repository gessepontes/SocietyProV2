using System.Collections.Generic;
using Dapper;
using SocietyProV2.Data.Repositories.Common;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;

namespace SocietyProV2.Data.Repositories
{
    public class JogadorInscritoRepository : RepositoryBase<JogadorInscrito>, IJogadorInscritoRepository
    {
        public IEnumerable<JogadorInscrito> GetAll(int idTime, int IDCampeonato) =>
            conn.Query<JogadorInscrito, Jogador, JogadorInscrito>(
                @"SELECT * from JogadorInscrito JI INNER JOIN Inscrito I ON JI.IDInscrito = I.ID INNER JOIN PreInscrito PI ON I.IDPreInscrito = PI.ID INNER JOIN Campeonato C ON C.IDCampeonato = PI.IDCampeonato AND C.IDCampeonato = @IDCampeonato RIGHT JOIN JOGADOR J ON JI.IDJOGADOR = J.ID WHERE J.IDTIME = @idTime AND J.STATUS = 1 AND (J.DATADISPENSA > C.dDataInicio OR  J.DATADISPENSA = '01/01/1900') AND J.DATACADASTRO <  C.dDataInicio",
                map: (jogadorInscrito, jogador) =>
                {
                    jogadorInscrito.Jogador = jogador;
                    return jogadorInscrito;
                },
                        param: new { idTime, IDCampeonato });
    }
}
