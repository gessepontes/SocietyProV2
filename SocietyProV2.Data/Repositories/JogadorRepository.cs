using Dapper;
using SocietyProV2.Data.Repositories.Common;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using System;

namespace SocietyProV2.Data.Repositories
{
    public class JogadorRepository : RepositoryBase<Jogador>, IJogadorRepository{
        public override void Add(Jogador obj)
        {
            string sql = "INSERT INTO JOGADOR(NOME,RG,DATANASCIMENTO,IDTIME,FOTO,TELEFONE,POSICAO,DISPENSADO,DATADISPENSA,STATUS,DATACADASTRO) ";
            sql = sql + "values(@NOME,@RG,@DATANASCIMENTO,@IDTIME,@FOTO,@TELEFONE,@POSICAO,@DISPENSADO,@DATADISPENSA,@STATUS,@DATACADASTRO);";

            if (obj.FOTO == "") obj.FOTO = "user.png";

            conn.Query(sql, new { obj.NOME, obj.RG, obj.DATANASCIMENTO,obj.IDTIME, obj.FOTO, obj.TELEFONE, obj.POSICAO, obj.DISPENSADO, obj.DATADISPENSA, obj.STATUS, obj.DATACADASTRO });


        }

        public override void Update(Jogador obj)
        {
            string sql = "";
            string parametros = "";

            if (obj.FOTO != "") parametros = parametros + ",FOTO=@FOTO";

            if (!obj.DISPENSADO)
            {
                obj.DATADISPENSA = Convert.ToDateTime("01/01/1900");
            }

            sql = "UPDATE JOGADOR SET NOME=@NOME,RG=@RG,DATANASCIMENTO=@DATANASCIMENTO,IDTIME=@IDTIME,TELEFONE=@TELEFONE,POSICAO=@POSICAO,DISPENSADO = @DISPENSADO,DATADISPENSA=@DATADISPENSA" + parametros + " WHERE ID = @ID; ";

            conn.Query(sql, new { obj.NOME, obj.RG, obj.DATANASCIMENTO, obj.IDTIME, obj.FOTO, obj.TELEFONE, obj.POSICAO, obj.DISPENSADO, obj.DATADISPENSA, obj.DATACADASTRO,obj.ID });
        }
    }
}
