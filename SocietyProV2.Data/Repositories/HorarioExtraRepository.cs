using Dapper;
using SocietyProV2.Data.Repositories.Common;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SocietyProV2.Data.Repositories
{
    public class HorarioExtraRepository : RepositoryBase<HorarioExtra>, IHorarioExtraRepository
    {
        public override IEnumerable<HorarioExtra> GetAll() =>
    conn.Query<HorarioExtra, CampoItem, Campo, HorarioExtra>(
        @"SELECT * FROM HORARIOEXTRA HE INNER JOIN CAMPOITEM CI ON HE.IDITEMCAMPO = CI.ID INNER JOIN CAMPO C ON CI.IDCAMPO = C.ID",
        map: (horarioExtra, campoItem, campo) =>
        {
            horarioExtra.CampoItem = campoItem;
            horarioExtra.CampoItem.Campo = campo;
            return horarioExtra;
        });

        public override HorarioExtra GetById(int? id) =>
    conn.Query<HorarioExtra, CampoItem, Campo, HorarioExtra>(
    @"SELECT TOP(1) * FROM HORARIOEXTRA HE INNER JOIN CAMPOITEM CI ON HE.IDITEMCAMPO = CI.ID INNER JOIN CAMPO C ON CI.IDCAMPO = C.ID WHERE HE.ID = @horarioID",
    map: (horarioExtra, campoItem, campo) =>
    {
        horarioExtra.CampoItem = campoItem;
        horarioExtra.CampoItem.Campo = campo;
        return horarioExtra;
    },
    param: new { horarioID = id }).FirstOrDefault();
    }
}
