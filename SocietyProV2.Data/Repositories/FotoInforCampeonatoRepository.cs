using Dapper;
using SocietyProV2.Data.Repositories.Common;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;

namespace SocietyProV2.Data.Repositories
{
    public class FotoInforCampeonatoRepository : RepositoryBase<FotoInforCampeonato>, IFotoInforCampeonatoRepository
    {
        public override void Add(FotoInforCampeonato obj)
        {
            string sql = "INSERT INTO FOTOINFORCAMPEONATO(NOME,ISEQUENCIA,IDCAMPEONATO,FOTO) ";
            sql = sql + "values(@NOME,@ISEQUENCIA,@IDCAMPEONATO,@FOTO);";

            if (obj.FOTO == "") obj.FOTO = "team.png";

            conn.Query(sql, new { obj.NOME, obj.ISEQUENCIA, obj.IDCAMPEONATO,obj.FOTO });


        }
    }
}
