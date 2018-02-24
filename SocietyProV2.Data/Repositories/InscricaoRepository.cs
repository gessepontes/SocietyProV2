using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using SocietyProV2.Data.Repositories.Common;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SocietyProV2.Data.Repositories
{
    public class InscricaoRepository : RepositoryBase<Inscricao>, IInscricaoRepository
    {
        public IEnumerable<Inscricao> GetPreInscritoAll(int idCampeoanto) =>
    conn.Query<PreInscricao, Time, Pessoa, Inscricao, Inscricao>(
        @"SELECT * FROM PreInscrito PI INNER JOIN Time T ON PI.IDTime = T.ID INNER JOIN PESSOA P ON T.IDPESSOA = P.ID LEFT JOIN Inscrito I  ON PI.ID = I.IDPreInscrito WHERE PI.IDCampeonato = @idCampeoanto",
        map: (preInscricao, time, pessoa, inscricao) =>
        {
            if (inscricao == null)
            {
                inscricao = new Inscricao();
            }

            inscricao.PreInscricao = preInscricao;
            inscricao.PreInscricao.Time = time;
            inscricao.PreInscricao.Time.Pessoa = pessoa;
            return inscricao;
        },
                param: new { idCampeoanto });


        public IEnumerable<Inscricao> GetAll(int idCampeoanto) =>
    conn.Query<Inscricao, PreInscricao, Time, Inscricao>(
        @"SELECT * FROM Inscrito I INNER JOIN PreInscrito PI ON I.IDPreInscrito = PI.ID INNER JOIN Time T ON PI.IDTime = T.ID WHERE PI.IDCampeonato = @idCampeoanto",
        map: (inscricao, preInscricao, time) =>
        {
            inscricao.PreInscricao = preInscricao;
            inscricao.PreInscricao.Time = time;
            return inscricao;
        },
                param: new { idCampeoanto });


        public SelectList GetDropAll(int idCampeoanto) =>
                new SelectList(conn.Query<Inscricao, PreInscricao, Time, Inscricao>(
                @"SELECT * FROM Inscrito I INNER JOIN PreInscrito PI ON I.IDPreInscrito = PI.ID INNER JOIN Time T ON PI.IDTime = T.ID WHERE PI.IDCampeonato = @idCampeoanto",
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


        public SelectList GetDropAllGrupo(int idCampeoanto) =>
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


        public SelectList GetDropEditGrupo(int id) =>
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
