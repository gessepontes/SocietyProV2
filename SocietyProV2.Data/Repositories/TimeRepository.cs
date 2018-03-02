using Dapper;
using SocietyProV2.Data.Repositories.Common;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SocietyProV2.Data.Repositories
{
    public class TimeRepository : RepositoryBase<Time>, ITimeRepository
    {
        public IEnumerable<Time> GetAllTimeDrop()
        {
            return conn.Query<Time>("SELECT ID,NOME FROM dbo.Time WHERE STATUS =1 ORDER BY NOME").ToList();
        }

        public override void Add(Time obj)
        {
            string sql = "INSERT INTO TIME(NOME,IDPESSOA,OBSERVACAO,DATAFUNDACAO,ATIVO,STATUS,DATACADASTRO,SIMBOLO,TIPO) ";
            sql = sql + "values(@NOME,@IDPESSOA,@OBSERVACAO,@DATAFUNDACAO,@ATIVO,@STATUS,@DATACADASTRO,@SIMBOLO,@TIPO);";

            if (obj.SIMBOLO == "") obj.SIMBOLO = "team.png";

            conn.Query(sql, new { obj.NOME, obj.IDPESSOA, obj.OBSERVACAO, obj.DATAFUNDACAO, obj.SIMBOLO, obj.ATIVO, obj.TIPO, obj.STATUS, obj.DATACADASTRO });


        }

        public override void Update(Time obj)
        {
            string sql = "";
            string parametros = "";

            if (obj.SIMBOLO != "") parametros = parametros + ",SIMBOLO=@SIMBOLO";

            sql = "UPDATE TIME SET NOME=@NOME,IDPESSOA=@IDPESSOA,OBSERVACAO=@OBSERVACAO,DATAFUNDACAO=@DATAFUNDACAO,ATIVO=@ATIVO,TIPO=@TIPO" + parametros + " WHERE ID = @ID; ";

            conn.Query(sql, new { obj.NOME, obj.IDPESSOA, obj.OBSERVACAO, obj.DATAFUNDACAO, obj.SIMBOLO, obj.ATIVO, obj.TIPO, obj.ID });
        }


    }
}
