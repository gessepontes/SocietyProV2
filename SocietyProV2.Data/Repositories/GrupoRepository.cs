using Dapper;
using SocietyProV2.Data.Repositories.Common;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocietyProV2.Data.Repositories
{
    public class GrupoRepository : RepositoryBase<Grupo>, IGrupoRepository
    {
        public override IEnumerable<Grupo> GetAll() =>
            conn.Query<Grupo, Inscricao, Time, Grupo>(
            @"select * from CampeonatoGrupo CG INNER JOIN Inscrito I ON CG.IDInscrito = I.ID INNER JOIN TIME T ON I.IDTime = T.ID",
            map: (grupo, inscricao, time) =>
            {
                grupo.Inscricao = inscricao;
                grupo.Inscricao.Time = time;
                return grupo;
            });


        public override Grupo GetById(int? id) =>
            conn.Query<Grupo, Inscricao, Time, Grupo>(
            @"select TOP(1) * from CampeonatoGrupo CG INNER JOIN Inscrito I ON CG.IDInscrito = I.ID INNER JOIN TIME T ON I.IDTime = T.ID WHERE CG.ID = @id",
            map: (grupo, inscricao, time) =>
            {
                grupo.Inscricao = inscricao;
                grupo.Inscricao.Time = time;
                return grupo;
            },
            param: new { id }).FirstOrDefault();

        public IEnumerable<Grupo> GetAllByCampeonato(int id) =>
            conn.Query<Grupo, Inscricao,  Time, Grupo>(
            @"select * from CampeonatoGrupo CG INNER JOIN Inscrito I ON CG.IDInscrito = I.ID INNER JOIN TIME T ON I.IDTime = T.ID WHERE I.IDCampeonato = @id",
            map: (grupo, inscricao,  time) =>
            {
                grupo.Inscricao = inscricao;
                grupo.Inscricao.Time = time;
                return grupo;
            }, param: new { id });

        public Grupo GetByIdInscricao(int id) =>
            conn.Query<Grupo>(
            @"select TOP(1) * from CampeonatoGrupo CG WHERE CG.IDInscrito = @id", param: new { id }).FirstOrDefault();


        public int CreateAutomatico(int iQuantidadeTimes, int IDCampeonato)
        {

            try
            {
                InscricaoRepository _inscricao = new InscricaoRepository();

                IEnumerable<Grupo> ListaGrupos = GetAllByCampeonato(IDCampeonato);

                if (ListaGrupos.Count() > 0)
                {
                    return 2;
                }

                Random randNum = new Random(Environment.TickCount);

                int qtde_times = 0;
                int qtde_grupos = 0;

                int cont = 0;

                IEnumerable<Inscricao> inscritos = _inscricao.GetAll(IDCampeonato);

                int[] times = new int[inscritos.Count()];

                foreach (Inscricao e in inscritos)
                {
                    times[cont] = e.ID;
                    cont++;
                }

                qtde_times = times.Count();

                if (qtde_times % iQuantidadeTimes == 0)
                {
                    qtde_grupos = qtde_times / iQuantidadeTimes;
                }
                else
                {
                    qtde_grupos = (qtde_times / iQuantidadeTimes) + 1;
                }

                cont = 0;

                for (int i = 1; i <= qtde_grupos; i++)
                {
                    for (int j = 1; j <= iQuantidadeTimes; j++)
                    {
                        Grupo campeonatoGrupo = new Grupo
                        {
                            IDInscrito = times[randNum.Next(0, qtde_times - 1)],
                            IDGrupo = (IDGrupo)i,
                            dDataCadastro = DateTime.Now
                        };

                        if (GetByIdInscricao(campeonatoGrupo.IDInscrito) == null)
                        {
                            Add(campeonatoGrupo);
                        }
                        else
                        {
                            if (i == qtde_grupos)
                            {
                                for (int z = 0; z < qtde_times; z++)
                                {
                                    Grupo ultimoCampeonatoGrupo = new Grupo
                                    {
                                        IDInscrito = times[z],
                                        IDGrupo = (IDGrupo)i,
                                        dDataCadastro = DateTime.Now
                                    };

                                    if (GetByIdInscricao(ultimoCampeonatoGrupo.IDInscrito) == null)
                                    {
                                        Add(ultimoCampeonatoGrupo);
                                    }

                                }

                                return 1;
                            }

                            j = j - 1;
                        }

                    }
                }
            }
            catch (Exception)
            {
                return 3;
            }

            return 1;
        }
    }
}
