using Dapper;
using SocietyProV2.Data.Repositories.Common;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SocietyProV2.Data.Repositories
{
    public class HorarioRepository : RepositoryBase<Horario>, IHorarioRepository
    {
        public override IEnumerable<Horario> GetAll() =>
    conn.Query<Horario, CampoItem, Campo, Horario>(
        @"SELECT * FROM HORARIOPADRAO HP INNER JOIN CAMPOITEM CI ON HP.IDITEMCAMPO = CI.ID INNER JOIN CAMPO C ON CI.IDCAMPO = C.ID",
        map: (horario, campoItem, campo) =>
        {
            horario.CampoItem = campoItem;
            horario.CampoItem.Campo = campo;
            return horario;
        });

        public override Horario GetById(int? id) =>
conn.Query<Horario, CampoItem, Campo, Horario>(
@"SELECT TOP(1) * FROM HORARIOPADRAO HP INNER JOIN CAMPOITEM CI ON HP.IDITEMCAMPO = CI.ID INNER JOIN CAMPO C ON CI.IDCAMPO = C.ID WHERE HP.ID = @horarioID",
map: (horario, campoItem, campo) =>
{
    horario.CampoItem = campoItem;
    horario.CampoItem.Campo = campo;
    return horario;
},
param: new { horarioID = id }).FirstOrDefault();
    }
}
