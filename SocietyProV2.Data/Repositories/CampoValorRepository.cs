using Dapper;
using SocietyProV2.Data.Repositories.Common;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SocietyProV2.Data.Repositories
{
    public class CampoValorRepository : RepositoryBase<CampoValor>, ICampoValorRepository
    {
        public override IEnumerable<CampoValor> GetAll() =>
            conn.Query<CampoValor, CampoItem, Campo, CampoValor>(
                @"SELECT * FROM CAMPOVALOR CV INNER JOIN CAMPOITEM CI ON CV.IDCAMPOITEM = CI.ID INNER JOIN CAMPO C ON CI.IDCAMPO = C.ID",
                map: (campovalor, campoItem, campo) =>
                {
                    campovalor.CampoItem = campoItem;
                    campovalor.CampoItem.Campo = campo;
                    return campovalor;
                });

        public override CampoValor GetById(int? id) =>
    conn.Query<CampoValor, CampoItem, Campo, CampoValor>(
        @"SELECT TOP(1) * FROM CAMPOVALOR CV INNER JOIN CAMPOITEM CI ON CV.IDCAMPOITEM = CI.ID INNER JOIN CAMPO C ON CI.IDCAMPO = C.ID WHERE CV.ID = @campoValorID",
        map: (campovalor, campoItem, campo) =>
        {
            campovalor.CampoItem = campoItem;
            campovalor.CampoItem.Campo = campo;
            return campovalor;
        },
        param: new { campoValorID = id }).FirstOrDefault();
    }
}
