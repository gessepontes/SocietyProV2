using Dapper;
using SocietyProV2.Data.Repositories.Common;
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


        public override Campeonato GetById(int? id) {
            Campeonato result = conn.Query<Campeonato>("SELECT * FROM CAMPEONATO C WHERE C.IDCampeonato = @id", new { id }).FirstOrDefault();
            ICollection<FotoInforCampeonato> resultInfor = conn.Query<FotoInforCampeonato>("SELECT * FROM FOTOINFORCAMPEONATO FC WHERE FC.IDCAMPEONATO = @id", new { id }).ToList();

            result.FotoInforCampeonato = resultInfor;
            return result;

        }
    }
}
