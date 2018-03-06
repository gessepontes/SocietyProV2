using Microsoft.Extensions.DependencyInjection;
using SocietyProV2.Data.Repositories;
using SocietyProV2.Domain.Interfaces.Repositories;

namespace SocietyProV2.IoC
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<ITimeRepository, TimeRepository>();
            services.AddScoped<IJogadorRepository, JogadorRepository>();
            services.AddScoped<IPartidaRepository, PartidaRepository>();
            services.AddScoped<ICampoRepository, CampoRepository>();
            services.AddScoped<IJogadorPartidaRepository, JogadorPartidaRepository>();
            services.AddScoped<IGolPartidaRepository, GolPartidaRepository>();
            services.AddScoped<ICidadeRepository, CidadeRepository>();
            services.AddScoped<ICampoItemRepository, CampoItemRepository>();
            services.AddScoped<ICampoValorRepository, CampoValorRepository>();
            services.AddScoped<ICampoHorarioRepository, CampoHorarioRepository>();
            services.AddScoped<IHorarioRepository, HorarioRepository>();
            services.AddScoped<IHorarioExtraRepository, HorarioExtraRepository>();
            services.AddScoped<IAgendarRepository, AgendarRepository>();
            services.AddScoped<ICampeonatoRepository, CampeonatoRepository>();
            services.AddScoped<IFotoInforCampeonatoRepository, FotoInforCampeonatoRepository>();
            services.AddScoped<IInscricaoRepository, InscricaoRepository>();
            services.AddScoped<IJogadorInscritoRepository, JogadorInscritoRepository>();
            services.AddScoped<IGrupoRepository, GrupoRepository>();
            services.AddScoped<IPartidaCampeonatoRepository, PartidaCampeonatoRepository>();
            services.AddScoped<ICartaoRepository, CartaoRepository>();
        }

    }
}
