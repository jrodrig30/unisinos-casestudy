using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Unisinos.CaseStudy.Business.Interfaces;
using Unisinos.CaseStudy.Business.Services;

namespace Unisinos.CaseStudy.Tests
{
    public class BaseTest
    {
        public IServiceProvider container;

        public BaseTest()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<IPropostaService, PropostaService>();
            serviceCollection.AddTransient<IClienteService, ClienteService>();

            serviceCollection.AddDbContext<Data.BemPromotoraContext>
                (options => options
                  .UseInMemoryDatabase("bem-promotora"));

            container = serviceCollection
                .AddLogging()
                .BuildServiceProvider();
        }
    }
}
