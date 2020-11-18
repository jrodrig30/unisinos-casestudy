using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unisinos.CaseStudy.Data.Models;

namespace Unisinos.CaseStudy.Data
{
    public static class DbInitializer
    {
        public static IWebHost InitializeAndSeed(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<BemPromotoraContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                SeedClientes(context);
                SeedUsuarios(context);
                SeedPropostas(context);
            }

            return host;
        }

        public static void SeedUsuarios(BemPromotoraContext context)
        {
            if (!context.Usuarios.Any())
            {
                var usuarios = new List<Usuario>
                {
                    new Usuario { Nome = "Felipe Machado", Email = "felipecmachado@outlook.com", Perfil = Models.Enums.Perfil.Administrator },
                    new Usuario { Nome = "Cassio Deon", Email = "cassiodeon@gmail.com", Perfil = Models.Enums.Perfil.Administrator },
                    new Usuario { Nome = "Stefani Lima", Email = "stefanilimakh@gmail.com", Perfil = Models.Enums.Perfil.Administrator },
                    new Usuario { Nome = "Jose Rodrigo Borges", Email = "joserodrigorf@gmail.com", Perfil = Models.Enums.Perfil.Administrator }
                };

                context.AddRange(usuarios);
                context.SaveChanges();
            }
        }

        public static void SeedClientes(BemPromotoraContext context)
        {
            if (!context.Clientes.Any())
            {
                var clientes = new List<Cliente>
                {
                    new Cliente { Nome = "Cliente A" },
                    new Cliente { Nome = "Cliente B" }
                };

                context.AddRange(clientes);
                context.SaveChanges();
            }
        }

        public static void SeedPropostas(BemPromotoraContext context)
        {
            if (!context.Propostas.Any())
            {
                var propostas = new List<Proposta>
                {
                    new Proposta { ClienteId = 1, DataCriacao = DateTime.Now, Status = PropostaStatus.Criada, Valor = 5000 },
                    new Proposta { ClienteId = 2, DataCriacao = DateTime.Now, Status = PropostaStatus.Aprovada, Valor = 15000 }
                };

                context.AddRange(propostas);
                context.SaveChanges();
            }
        }

    }
}
        