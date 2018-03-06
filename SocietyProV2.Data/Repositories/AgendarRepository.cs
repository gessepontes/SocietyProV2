using Dapper;
using SocietyProV2.Data.Repositories.Common;
using SocietyProV2.Domain.Diversos;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;


namespace SocietyProV2.Data.Repositories
{
    public class AgendarRepository : RepositoryBase<Agendar>, IAgendarRepository
    {
        public IEnumerable<Agendamento> GetAllAgendamento()
        {
            string query;

            query = "SELECT HA.ID,HA.DATA,HA.STATUS,HP.HORARIO HORA,C.NOME CAMPO,CASE WHEN P.NOME IS NULL THEN HA.CLIENTENAOCADASTRADO ELSE P.NOME END PESSOA,CASE WHEN P.TELEFONE IS NULL THEN HA.TELEFONE ELSE P.TELEFONE END TELEFONE ";
            query += "FROM HORARIOAGENDADO HA INNER JOIN HORARIOPADRAO HP ON HA.IDHORARIO = HP.ID AND HA.TIPOHORARIO = 1 INNER JOIN CAMPOITEM CI ON CI.ID = HP.IDITEMCAMPO INNER JOIN CAMPO C ON C.ID = CI.IDCAMPO LEFT JOIN PESSOA P ON HA.IDPESSOA = P.ID ";
            query += "UNION ";
            query += "SELECT HA.ID,HA.DATA,HA.STATUS,HE.HORARIO HORA,C.NOME CAMPO,CASE WHEN P.NOME IS NULL THEN HA.CLIENTENAOCADASTRADO ELSE P.NOME END PESSOA,CASE WHEN P.TELEFONE IS NULL THEN HA.TELEFONE ELSE P.TELEFONE END TELEFONE ";
            query += "FROM HORARIOAGENDADO HA INNER JOIN HORARIOEXTRA HE ON HA.IDHORARIO = HE.ID AND HA.TIPOHORARIO = 2 INNER JOIN CAMPOITEM CI ON CI.ID = HE.IDITEMCAMPO INNER JOIN CAMPO C ON C.ID = CI.IDCAMPO LEFT JOIN PESSOA P ON HA.IDPESSOA = P.ID ";
            query += "ORDER BY DATA DESC,HORA DESC ";

            return conn.Query<Agendamento>(query).ToList();
        }

        public Agendamento GetByIdAgendamento(int? id)
        {
            string query;

            query = "SELECT HA.ID,HA.DATA,HA.STATUS,HP.HORARIO HORA,C.NOME CAMPO,CASE WHEN P.NOME IS NULL THEN HA.CLIENTENAOCADASTRADO ELSE P.NOME END PESSOA,CASE WHEN P.TELEFONE IS NULL THEN HA.TELEFONE ELSE P.TELEFONE END TELEFONE ";
            query += "FROM HORARIOAGENDADO HA INNER JOIN HORARIOPADRAO HP ON HA.IDHORARIO = HP.ID AND HA.TIPOHORARIO = 1 INNER JOIN CAMPOITEM CI ON CI.ID = HP.IDITEMCAMPO INNER JOIN CAMPO C ON C.ID = CI.IDCAMPO LEFT JOIN PESSOA P ON HA.IDPESSOA = P.ID ";
            query += "WHERE HA.ID = @id ";
            query += "UNION ";
            query += "SELECT HA.ID,HA.DATA,HA.STATUS,HE.HORARIO HORA,C.NOME CAMPO,CASE WHEN P.NOME IS NULL THEN HA.CLIENTENAOCADASTRADO ELSE P.NOME END PESSOA,CASE WHEN P.TELEFONE IS NULL THEN HA.TELEFONE ELSE P.TELEFONE END TELEFONE ";
            query += "FROM HORARIOAGENDADO HA INNER JOIN HORARIOEXTRA HE ON HA.IDHORARIO = HE.ID AND HA.TIPOHORARIO = 2 INNER JOIN CAMPOITEM CI ON CI.ID = HE.IDITEMCAMPO INNER JOIN CAMPO C ON C.ID = CI.IDCAMPO LEFT JOIN PESSOA P ON HA.IDPESSOA = P.ID ";
            query += "WHERE HA.ID = @id ";

            return conn.Query<Agendamento>(query, new { id }).FirstOrDefault();
        }

        public void Status(int id, char status) => conn.Execute("UPDATE HORARIOAGENDADO SET STATUS=@status  WHERE ID = @id; ", new { status, id });

        public IEnumerable<Agendamento> GetHorarios(DateTime date, int idItemCampo, TipoHorario idTipo)
        {
            string query;

            if ((int)idTipo == 1) {
                query = "SELECT HP.ID,HP.HORARIO HORA FROM HORARIOPADRAO HP WHERE HP.IDITEMCAMPO = @idItemCampo AND HP.DIASEMANA = DATEPART(DW,@date) - 1 AND HP.ID NOT IN (select IDHORARIO FROM HORARIOAGENDADO WHERE DATA = @date AND TIPOHORARIO = 1 AND STATUS IN ('P','A')) ";
            }
            else {
                query = "SELECT HE.ID,HE.HORARIO HORA FROM HORARIOEXTRA HE WHERE HE.IDITEMCAMPO = @idItemCampo AND HE.DATA = @date AND HE.ID NOT IN (select IDHORARIO from HORARIOAGENDADO WHERE DATA = @date AND TIPOHORARIO = 2 AND STATUS IN ('P','A')) ";
            } 
                                   
            query += "ORDER BY HORA ";

            return conn.Query<Agendamento>(query, new { date, idItemCampo }).ToList();
        }
    }
}
