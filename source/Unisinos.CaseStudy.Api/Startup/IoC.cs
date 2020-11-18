using Microsoft.Extensions.DependencyInjection;
using Unisinos.CaseStudy.Business.Interfaces;
using Unisinos.CaseStudy.Business.Services;

namespace Unisinos.CaseStudy.API.Startup
{
    public static class IoC
    {
        // https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2
        // DI é nativo no .NET Core, o que permite alcançar IoC com uma certa facilidade.
        // Aqui acontece a configuração do Container
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IPropostaService, PropostaService>();

            return services;
        }
    }
}
