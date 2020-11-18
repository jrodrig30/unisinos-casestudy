using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Unisinos.CaseStudy.API.Startup
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Unisinos - Software Engineering Postgraduate Degree - Case Study",
                    TermsOfService = "None",
                    Contact = new Contact()
                    {
                        Name = "Felipe Coelho Machado, Cassio Deon, Stefani Lima, Jose Rodrigo Borges",
                        Email = "felipecmachado@outlook.com; cassiodeon@gmail.com; stefanilimakh@gmail.com; joserodrigorf@gmail.com"
                    },
                    License = new License { Name = "MIT", Url = "https://github.com/felipecmachado/unisinos-casestudy/blob/master/LICENSE" }
                });
            });
        }
    }
}
