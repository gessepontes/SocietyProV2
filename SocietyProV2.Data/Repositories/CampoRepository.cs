using Dapper;
using SocietyProV2.Data.Repositories.Common;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SocietyProV2.Data.Repositories
{
    public class CampoRepository : RepositoryBase<Campo>, ICampoRepository
    {
        public override void Add(Campo obj)
        {
            string sql = "INSERT INTO CAMPO(NOME,ENDERECO,TELEFONE,VALOR,VALORMENSAL,STATUS,DATACADASTRO,SOCIETY,CAMPO11,AGENDAMENTO,LOGO,IDPESSOA,IDCIDADE) ";
            sql = sql + "values(@NOME,@ENDERECO,@TELEFONE,@VALOR,@VALORMENSAL,@STATUS,@DATACADASTRO,@SOCIETY,@CAMPO11,@AGENDAMENTO,@LOGO,@IDPESSOA,@IDCIDADE);";

            if (obj.LOGO == "") obj.LOGO = "user.png";

            conn.Query(sql, new { obj.NOME, obj.ENDERECO, obj.TELEFONE, obj.VALOR, obj.VALORMENSAL, obj.STATUS, obj.DATACADASTRO, obj.SOCIETY, obj.CAMPO11, obj.AGENDAMENTO, obj.LOGO, obj.IDPESSOA, obj.IDCIDADE });
        }

        public override void Update(Campo obj)
        {
            string sql = "";
            string parametros = "";

            if (obj.LOGO != "") parametros = parametros + ",LOGO=@LOGO";

            sql = "UPDATE CAMPO SET NOME=@NOME,ENDERECO=@ENDERECO,TELEFONE=@TELEFONE,VALOR=@VALOR,VALORMENSAL=@VALORMENSAL,STATUS=@STATUS,DATACADASTRO=@DATACADASTRO,SOCIETY=@SOCIETY,CAMPO11=@CAMPO11,AGENDAMENTO=@AGENDAMENTO,IDPESSOA=@IDPESSOA,IDCIDADE=@IDCIDADE" + parametros + " WHERE ID = @ID; ";

            conn.Query(sql, new { obj.NOME, obj.ENDERECO, obj.TELEFONE, obj.VALOR, obj.VALORMENSAL, obj.STATUS, obj.DATACADASTRO, obj.SOCIETY, obj.CAMPO11, obj.AGENDAMENTO, obj.LOGO, obj.IDPESSOA, obj.IDCIDADE, obj.ID });
        }

        public IEnumerable<Campo> GetAllCampoDrop(string sTipo = "")
        {
            string parametros = "";

            switch (sTipo)
            {
                case "SOCIETY":
                    parametros = " AND SOCIETY = 1";
                    break;
                case "CAMPO11":
                    parametros = " AND CAMPO11 = 1";
                    break;
                default:
                    parametros = "";
                    break;
            }


            return conn.Query<Campo>("SELECT ID,NOME FROM dbo.Campo WHERE STATUS =1 " + parametros + " ORDER BY NOME").ToList();
        }
    }
}
