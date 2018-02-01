using SocietyProV2.Data.Mappings;
using SocietyProV2.Domain.Interfaces.Repositories.Common;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using Dommel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace SocietyProV2.Data.Repositories.Common
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly SqlConnection conn;

        public RepositoryBase()
        {
            if (FluentMapper.EntityMaps.IsEmpty)
            {
                FluentMapper.Initialize(c =>
                {
                    c.AddMap(new PessoaMap());
                    c.AddMap(new PessoaPerfilMap());
                    c.AddMap(new TimeMap());
                    c.AddMap(new JogadorMap());
                    c.AddMap(new PartidaMap());
                    c.AddMap(new CampoMap());
                    c.AddMap(new JogadorPartidaMap());
                    c.AddMap(new GolPartidaMap());
                    c.AddMap(new CidadeMap());
                    c.AddMap(new CampoItemMap());
                    c.AddMap(new CampoValorMap());
                    c.AddMap(new CampoHorarioMap());
                    c.AddMap(new HorarioMap());
                    c.AddMap(new HorarioExtraMap());
                    c.AddMap(new AgendarMap());
                    c.AddMap(new CampeonatoMap());
                    c.AddMap(new FotoInforCampeonatoMap());
                    c.AddMap(new PreInscricaoMap());
                    c.AddMap(new InscricaoMap());
                    c.AddMap(new JogadorInscritoMap());
                    c.ForDommel();
                });
            }

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();


            conn = new SqlConnection(config.GetSection(key: "ConnectionStrings")["DefaultConnection"]);

        }

        public virtual void Add(TEntity obj) => conn.Insert(obj);

        public virtual IEnumerable<TEntity> GetAll() => conn.GetAll<TEntity>();

        public virtual TEntity GetById(int? id) => conn.Get<TEntity>(id);

        public virtual void Remove(TEntity obj) => conn.Delete(obj);

        public virtual void Update(TEntity obj) => conn.Update(obj);

        public void Dispose()
        {
            conn.Close();
            conn.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
