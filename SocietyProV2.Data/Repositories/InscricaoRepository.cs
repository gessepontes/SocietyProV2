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
        public IEnumerable<Inscricao> GetAll(int idCampeoanto) =>
    conn.Query<Inscricao, Time, Pessoa, Inscricao>(
        @"SELECT * FROM Inscrito I INNER JOIN Time T ON I.IDTime = T.ID INNER JOIN Pessoa P ON P.ID = T.IDPESSOA  WHERE I.IDCampeonato = @idCampeoanto",
        map: (inscricao, time, pessoa) =>
        {
            inscricao.Time = time;
            inscricao.Time.Pessoa = pessoa;
            return inscricao;
        },
                param: new { idCampeoanto });


        public SelectList GetDropAll(int idCampeoanto) =>
                new SelectList(conn.Query<Inscricao, Time, Inscricao>(
                @"SELECT * FROM Inscrito I INNER JOIN Time T ON I.IDTime = T.ID WHERE I.IDCampeonato = @idCampeoanto",
                map: (inscricao, time) =>
                {
                    inscricao.Time = time;
                    return inscricao;
                },
                        param: new { idCampeoanto }).Select(x => new SelectListItem
                        {
                            Text = x.Time.NOME.ToUpper(),
                            Value = x.ID.ToString()
                        }).ToList().OrderBy(p => p.Text), "Value", "Text");


        public SelectList GetDropAllGrupo(int idCampeoanto) =>
            new SelectList(conn.Query<Inscricao, Time, Inscricao>(
                @"SELECT * FROM Inscrito I INNER JOIN Time T ON I.IDTime = T.ID WHERE I.IDCampeonato = @idCampeoanto AND I.ID NOT IN (SELECT IDInscrito FROM CampeonatoGrupo)",
                map: (inscricao, time) =>
                {
                    inscricao.Time = time;
                    return inscricao;
                },
                param: new { idCampeoanto }).Select(x => new SelectListItem
                {
                    Text = x.Time.NOME.ToUpper(),
                    Value = x.ID.ToString()
                }).ToList().OrderBy(p => p.Text), "Value", "Text");


        public SelectList GetDropEditGrupo(int id) =>
                new SelectList(conn.Query<Inscricao, Time, Inscricao>(
                    @"SELECT * FROM Inscrito I INNER JOIN Time T ON I.IDTime = T.ID WHERE I.id = @id",
                    map: (inscricao, time) =>
                    {
                        inscricao.Time = time;
                        return inscricao;
                    },
                            param: new { id }).Select(x => new SelectListItem
                            {
                                Text = x.Time.NOME.ToUpper(),
                                Value = x.ID.ToString()
                            }).ToList().OrderBy(p => p.Text), "Value", "Text");


        public void Ativar(int id) => conn.Execute(@"UPDATE Inscrito SET bAtivo = 1  WHERE ID = @id", param: new { id });
    }
}
