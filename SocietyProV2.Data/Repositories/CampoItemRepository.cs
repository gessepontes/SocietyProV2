using Dapper;
using SocietyProV2.Data.Repositories.Common;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SocietyProV2.Data.Repositories
{
    public class CampoItemRepository : RepositoryBase<CampoItem>, ICampoItemRepository
    {
        public override IEnumerable<CampoItem> GetAll() =>
            conn.Query<CampoItem, Campo, CampoItem>(
                @"SELECT * FROM CAMPOITEM CI INNER JOIN CAMPO C ON CI.IDCAMPO = C.ID",
                map: (campoItem, campo) =>
                {
                    campoItem.Campo = campo;
                    return campoItem;
                });

        public override CampoItem GetById(int? id) =>
    conn.Query<CampoItem, Campo, CampoItem>(
        @"SELECT TOP(1) * FROM CAMPOITEM CI INNER JOIN CAMPO C ON CI.IDCAMPO = C.ID WHERE CI.ID = @campoItemID",
        map: (campoItem, campo) =>
        {
            campoItem.Campo = campo;
            return campoItem;
        },
        param: new { campoItemID = id }).FirstOrDefault();


        public IEnumerable<CampoItem> GetAllCampoItemDrop()
        {
            return conn.Query<CampoItem>("SELECT CI.ID, (C.NOME + ' - Campo: ' + CI.DESCRICAO) DESCRICAO FROM CAMPOITEM CI INNER JOIN CAMPO C ON CI.IDCAMPO = C.ID ORDER BY NOME").ToList();
        }

        public IEnumerable<CampoItem> GetByIdCampo(int id)
        {
            string query;

            query = "SELECT CI.ID, CI.DESCRICAO FROM CAMPOITEM CI WHERE CI.IDCAMPO=@id ORDER BY DESCRICAO";

            return conn.Query<CampoItem>(query, new { id }).ToList();
        }
    }
}
