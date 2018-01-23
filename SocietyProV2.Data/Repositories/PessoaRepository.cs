using Dapper;
using SocietyProV2.Data.Repositories.Common;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SocietyProV2.Data.Repositories
{
    public class PessoaRepository : RepositoryBase<Pessoa>, IPessoaRepository
    {
        public override void Add(Pessoa obj)
        {
            string sql = "INSERT INTO PESSOA(NOME,RG,CPF,DATANASCIMENTO,TELEFONE,EMAIL,FOTO,SENHA,ATIVO,CONFIRMACAO,SECURITYSTAMP,PERFIL,STATUS,DATACADASTRO) ";
            sql = sql + "values(@NOME,@RG,@CPF,@DATANASCIMENTO,@TELEFONE,@EMAIL,@FOTO,@SENHA,@ATIVO,@CONFIRMACAO,@SECURITYSTAMP,@PERFIL,@STATUS,@DATACADASTRO); SELECT CAST(SCOPE_IDENTITY() as int)";

            if (obj.FOTO == "") obj.FOTO = "user.png";

            var returnId = conn.Query<int>(sql, new { obj.NOME, obj.RG, obj.CPF, obj.DATANASCIMENTO, obj.TELEFONE, obj.EMAIL, obj.FOTO, obj.SENHA, obj.ATIVO, obj.CONFIRMACAO, obj.SECURITYSTAMP, obj.PERFIL, obj.STATUS, obj.DATACADASTRO }).SingleOrDefault();

            if (returnId != 0 && obj.PERFILSELECIONADO.Count > 0)
            {
                foreach (var perfil in obj.PERFILSELECIONADO)
                {
                    conn.Execute(@"INSERT PESSOAPERFIL(IDPESSOA,TIPOPERFIL) values(@IDPESSOA,@TIPOPERFIL)", new { IDPESSOA = returnId, TIPOPERFIL = perfil });
                }

            }
        }

        public override void Update(Pessoa obj)
        {
            string sql = "";
            string parametros = "";

            if (obj.SENHA != null) parametros = ",SENHA=@SENHA";
            if (obj.FOTO != "") parametros = parametros + ",FOTO=@FOTO";

            sql = "UPDATE PESSOA SET NOME=@NOME,RG=@RG,CPF=@CPF,DATANASCIMENTO=@DATANASCIMENTO,TELEFONE=@TELEFONE,EMAIL=@EMAIL,ATIVO = @ATIVO,CONFIRMACAO = @CONFIRMACAO" + parametros + " WHERE ID = @ID; ";

            conn.Execute(sql, new { obj.NOME, obj.RG, obj.CPF, obj.DATANASCIMENTO, obj.TELEFONE, obj.EMAIL, obj.FOTO, obj.SENHA, obj.ATIVO, obj.CONFIRMACAO, obj.ID });

            conn.Execute("DELETE FROM PESSOAPERFIL WHERE IDPESSOA = @ID; ", new { obj.ID });

            foreach (var perfil in obj.PERFILSELECIONADO)
            {
                conn.Execute(@"INSERT PESSOAPERFIL(IDPESSOA,TIPOPERFIL) values(@IDPESSOA,@TIPOPERFIL)", new { IDPESSOA = obj.ID, TIPOPERFIL = perfil });
            }
        }

        public void UpdateUser(Pessoa obj)
        {
            string sql = "";
            string parametros = "";

            if (obj.SENHA != null) parametros = ",SENHA=@SENHA";
            if (obj.FOTO != "") parametros = parametros + ",FOTO=@FOTO";

            sql = "UPDATE PESSOA SET NOME=@NOME,RG=@RG,DATANASCIMENTO=@DATANASCIMENTO,TELEFONE=@TELEFONE" + parametros + " WHERE ID = @ID; ";

            conn.Execute(sql, new { obj.NOME, obj.RG, obj.DATANASCIMENTO, obj.TELEFONE, obj.FOTO, obj.SENHA,obj.ID });
        }

        public Pessoa GetByIdPessoaPerfil(int? id)
        {
            Pessoa p = GetById(id);
            p.PESSOAPERFIL = conn.Query<PessoaPerfil>("SELECT TIPOPERFIL FROM dbo.PessoaPerfil WHERE IDPESSOA = " + id).ToList();
            return p;
        }


        public IEnumerable<Pessoa> GetAllPessoaDrop()
        {
            return conn.Query<Pessoa>("SELECT ID,NOME FROM dbo.Pessoa WHERE STATUS =1 AND PERFIL = 0 ORDER BY NOME").ToList();
        }

        //public override IEnumerable<Pessoa> GetAll() =>
        //    conn.Query<Pessoa, PessoaPerfil, Pessoa>(
        //        @"SELECT * FROM PESSOA P INNER JOIN PESSOAPERFIL PP ON P.ID = PP.IDPESSOA",
        //        map: (pessoa, pessoaPerfil) =>
        //        {
        //            //pessoa.PESSOAPERFIL = (ICollection<PessoaPerfil>)pessoaPerfil;
        //            return pessoa;
        //        });
    }
}
