using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using System;
using Unisinos.CaseStudy.Business.Interfaces;
using Unisinos.CaseStudy.Business;
using System.Threading.Tasks;
using Unisinos.CaseStudy.Business.Services;
using Unisinos.CaseStudy.Data;
using Unisinos.CaseStudy.API.Shared;
using Unisinos.CaseStudy.Data.Models;

namespace Unisinos.CaseStudy.Tests
{
    /// <summary>
    /// SENDO um consultor de vendas 
    /// POSSO cadastrar propostas de cr�dito consignado
    /// PARA verificar a possibilidade de disponibilizar o valor de um cr�dito ao cliente
    /// </summary>
    [TestFixture]
    public class CadastroPropostaTests : BaseTest
    {
        public readonly IPropostaService propostaService;
        public readonly IClienteService clienteService;

        public CadastroPropostaTests() : base()
        {
            this.clienteService = container.GetService<IClienteService>();
            this.propostaService = container.GetService<IPropostaService>();
        }

        [Test]
        public async Task PropostaDeveSerCriadaComSucesso()
        {
            var mockCliente = new Shared.Requests.ClienteRequest()
            {
                CPF = "025.658.650-07",
                Nome = "Felipe Machado",
                Email = "felipecmachado@outlook.com",
                DataNascimento = new DateTime(1990, 05, 04),
                Sexo = "Masculino"
            };

            var cliente = await clienteService.AddCliente(mockCliente);

            var proposta = await propostaService.AddProposta(
                new Shared.Requests.PropostaRequest()
                {
                    ClienteId = cliente.Item.ClienteId,
                    ResponsavelId = 1,
                    Valor = 15000
                });

            Assert.IsTrue(proposta.Item.Status == PropostaStatus.Criada);
        }

        [Test]
        public async Task PropostaDeveFalharPor_PorLimitePorCPF()
        {
            var expectedResponse = "ExcedeuNumeroPropostas";

            var mockCliente = new Shared.Requests.ClienteRequest()
            {
                CPF = "018.461.340-05",
                Nome = "José Rodrigo Borges",
                Email = "joserodrigorf@gmail.com",
                DataNascimento = new DateTime(1988, 03, 18),
                Sexo = "Masculino"
            };

            var cliente = await clienteService.AddCliente(mockCliente);

            var mockRequest = new Shared.Requests.PropostaRequest()
            {
                ClienteId = cliente.Item.ClienteId,
                ResponsavelId = 1,
                Valor = 15000
            };

            Response<Proposta> response = null;
            for (int i = 0; i < 6; i++)
            {
                await propostaService.AddProposta(mockRequest);
                response = await propostaService.AddProposta(mockRequest);
            }

            Assert.IsTrue(response.Code == ResponseCode.Error && response.ResponseStatus.HasError(expectedResponse));
        }

        [Test]
        public async Task PropostaDeveFalharPor_ValorMaximoDeCredito()
        {
            var expectedResponse = "ValorMaximo";

            var mockCliente = new Shared.Requests.ClienteRequest()
            {
                CPF = "025.078.690-70",
                Nome = "Cassio Deon",
                Email = "cassiodeon@gmail.com",
                DataNascimento = new DateTime(1990, 10, 04),
                Sexo = "Masculino"
            };

            var cliente = await clienteService.AddCliente(mockCliente);

            var mockRequest = new Shared.Requests.PropostaRequest()
            {
                ClienteId = cliente.Item.ClienteId,
                ResponsavelId = 1,
                Valor = 70000
            };

            var response = await propostaService.AddProposta(mockRequest);

            Assert.IsTrue(response.Code == ResponseCode.Error && response.ResponseStatus.HasError(expectedResponse));
        }

        [Test]
        public async Task PropostaDeveFalharPor_ValorMinimoDeCredito()
        {
            // Arrange
            var expectedResponse = "ValorMinimo";

            var mockCliente = new Shared.Requests.ClienteRequest()
            {
                CPF = "851.362.630-91",
                Nome = "Stefani Lima",
                Email = "stefanisilvadelima@hotmail.com",
                DataNascimento = new DateTime(1995, 11, 14),
                Sexo = "Feminino"
            };

            // Act
            var cliente = await clienteService.AddCliente(mockCliente);

            var mockRequest = new Shared.Requests.PropostaRequest()
            {
                ClienteId = cliente.Item.ClienteId,
                ResponsavelId = 1,
                Valor = 200
            };

            var response = await propostaService.AddProposta(mockRequest);

            // Assert
            Assert.IsTrue(response.Code == ResponseCode.Error && response.ResponseStatus.HasError(expectedResponse));
        }

        [Test]
        public async Task PropostaDeveFalharPor_CPFEmSituacaoIrregular()
        {
            var expectedResponse = "CpfIrregular";

            var mockCliente = new Shared.Requests.ClienteRequest()
            {
                CPF = "025.400.200-54",
                Nome = "Cassio Deon",
                Email = "cassio@gmail.com",
                DataNascimento = new DateTime(1985, 04, 17),
                Sexo = "Masculino"
            };

            var cliente = await clienteService.AddCliente(mockCliente);

            var mockRequest = new Shared.Requests.PropostaRequest()
            {
                ClienteId = cliente.Item.ClienteId,
                ResponsavelId = 1,
                Valor = 6000
            };

            var idProposta = (await propostaService.AddProposta(mockRequest)).Item.PropostaId;

            var response = await propostaService.ValidateProposta(idProposta);

            Assert.IsTrue(response.ResponseStatus.HasError(expectedResponse) && response.Item.Status == PropostaStatus.PendenciaAutomatica);
        }

        [Test]
        public async Task PropostaDeveFalharPor_IdadeMinima()
        {
            var expectedResponse = "IdadeMinima";

            var mockCliente = new Shared.Requests.ClienteRequest()
            {
                CPF = "533.495.020-67",
                Nome = "Senhor Novinho da Silva",
                Email = "juvenil@gmail.com",
                DataNascimento = new DateTime(2005, 02, 17), // Devia estar estudando
                Sexo = "Masculino"
            };

            var cliente = await clienteService.AddCliente(mockCliente);

            var mockRequest = new Shared.Requests.PropostaRequest()
            {
                ClienteId = cliente.Item.ClienteId,
                ResponsavelId = 1,
                Valor = 6000
            };

            var response = await propostaService.AddProposta(mockRequest);

            Assert.IsTrue(response.Code == ResponseCode.Error && response.ResponseStatus.HasError(expectedResponse));
        }

        [Test]
        public async Task PropostaDeveFalharPor_IdadeMaxima()
        {
            var expectedResponse = "IdadeMaxima";

            var mockCliente = new Shared.Requests.ClienteRequest()
            {
                CPF = "921.181.790-06",
                Nome = "Senhor Velhinho da Silva",
                Email = "idoso@gmail.com",
                DataNascimento = new DateTime(1921, 09, 01), // Ja devia ter morrido
                Sexo = "Masculino"
            };

            var cliente = await clienteService.AddCliente(mockCliente);

            var mockRequest = new Shared.Requests.PropostaRequest()
            {
                ClienteId = cliente.Item.ClienteId,
                ResponsavelId = 1,
                Valor = 15000
            };

            var response = await propostaService.AddProposta(mockRequest);

            Assert.IsTrue(response.Code == ResponseCode.Error && response.ResponseStatus.HasError(expectedResponse));
        }

        [Test]
        public void PropostaDeveFalharPor_PendenciaDocumentos()
        {
            Assert.Pass();
        }

        [TearDown]
        public void Dispose()
        {
            // TODO: Implement dispose
            var db = this.container.GetService<BemPromotoraContext>();
        }
    }
}