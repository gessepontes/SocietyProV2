using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using SocietyProV2.Data.Repositories.Common;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SocietyProV2.Data.Repositories
{
    public class GrupoRepository : RepositoryBase<Grupo>, IGrupoRepository
    {
        public override IEnumerable<Grupo> GetAll() =>
            conn.Query<Grupo, Inscricao, PreInscricao, Time, Grupo>(
            @"select * from CampeonatoGrupo CG INNER JOIN Inscrito I ON CG.IDInscrito = I.ID INNER JOIN PreInscrito PI ON I.IDPreInscrito = PI.ID INNER JOIN TIME T ON PI.IDTime = T.ID",
            map: (grupo, inscricao, preinscricao, time) =>
            {
                grupo.Inscricao = inscricao;
                grupo.Inscricao.PreInscricao = preinscricao;
                grupo.Inscricao.PreInscricao.Time = time;
                return grupo;
            });


        public override Grupo GetById(int? id) =>
            conn.Query<Grupo, Inscricao, PreInscricao, Time, Grupo>(
            @"select TOP(1) * from CampeonatoGrupo CG INNER JOIN Inscrito I ON CG.IDInscrito = I.ID INNER JOIN PreInscrito PI ON I.IDPreInscrito = PI.ID INNER JOIN TIME T ON PI.IDTime = T.ID WHERE CG.ID = @id",
            map: (grupo, inscricao, preinscricao, time) =>
            {
                grupo.Inscricao = inscricao;
                grupo.Inscricao.PreInscricao = preinscricao;
                grupo.Inscricao.PreInscricao.Time = time;
                return grupo;
            },
            param: new { id }).FirstOrDefault();

        public IEnumerable<Grupo> GetAllByCampeonato(int id) =>
            conn.Query<Grupo, Inscricao, PreInscricao, Time, Grupo>(
            @"select * from CampeonatoGrupo CG INNER JOIN Inscrito I ON CG.IDInscrito = I.ID INNER JOIN PreInscrito PI ON I.IDPreInscrito = PI.ID INNER JOIN TIME T ON PI.IDTime = T.ID WHERE PI.IDCampeonato = @id ",
            map: (grupo, inscricao, preinscricao, time) =>
            {
                grupo.Inscricao = inscricao;
                grupo.Inscricao.PreInscricao = preinscricao;
                grupo.Inscricao.PreInscricao.Time = time;
                return grupo;
            }, param: new { id });


        public SelectList GetDropAll(int idCampeoanto) =>
    new SelectList(conn.Query<Inscricao, PreInscricao, Time, Inscricao>(
        @"SELECT * FROM Inscrito I INNER JOIN PreInscrito PI ON I.IDPreInscrito = PI.ID INNER JOIN Time T ON PI.IDTime = T.ID WHERE PI.IDCampeonato = @idCampeoanto AND I.ID NOT IN (SELECT IDInscrito FROM CampeonatoGrupo) ",
        map: (inscricao, preInscricao, time) =>
        {
            inscricao.PreInscricao = preInscricao;
            inscricao.PreInscricao.Time = time;
            return inscricao;
        },
                param: new { idCampeoanto }).Select(x => new SelectListItem
                {
                    Text = x.PreInscricao.Time.NOME.ToUpper(),
                    Value = x.ID.ToString()
                }).ToList().OrderBy(p => p.Text), "Value", "Text");


        public SelectList GetDropEdit(int id) =>
    new SelectList(conn.Query<Inscricao, PreInscricao, Time, Inscricao>(
        @"SELECT * FROM Inscrito I INNER JOIN PreInscrito PI ON I.IDPreInscrito = PI.ID INNER JOIN Time T ON PI.IDTime = T.ID WHERE I.id = @id ",
        map: (inscricao, preInscricao, time) =>
        {
            inscricao.PreInscricao = preInscricao;
            inscricao.PreInscricao.Time = time;
            return inscricao;
        },
                param: new { id }).Select(x => new SelectListItem
                {
                    Text = x.PreInscricao.Time.NOME.ToUpper(),
                    Value = x.ID.ToString()
                }).ToList().OrderBy(p => p.Text), "Value", "Text");
    }
}
