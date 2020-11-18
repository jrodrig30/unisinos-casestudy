using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unisinos.CaseStudy.API.Shared;
using Unisinos.CaseStudy.Business.Helpers;
using Unisinos.CaseStudy.Business.Interfaces;
using Unisinos.CaseStudy.Data;
using Unisinos.CaseStudy.Data.Models;
using Unisinos.CaseStudy.Shared.Domain;
using Unisinos.CaseStudy.Shared.Exceptions;
using Unisinos.CaseStudy.Shared.Requests;

namespace Unisinos.CaseStudy.Business.Services
{
    public class PropostaService : BaseService, IPropostaService
    {
        public PropostaService(BemPromotoraContext context) : base(context) { }

        public async Task<Response<Proposta>> AddProposta(PropostaRequest request)
        {
            var response = new Response<Proposta>();

            try
            {
                var regras = new PropostaRegras();

                var cliente = await this.Context.Clientes.FirstOrDefaultAsync(x => x.ClienteId == request.ClienteId);

                if (cliente == null)
                    throw new BusinessException("ClienteNaoExiste", "O Cliente não existe no banco de dados.");

                regras.QuantidadePropostasAcimaPermitido(await this.Context.Propostas.CountAsync(x => x.ClienteId == cliente.ClienteId));

                regras.IdadeDentroDosLimites(cliente.Idade);

                regras.ValorDentroDosLimites(request.Valor);

                var obj = new Proposta
                {
                    ClienteId = request.ClienteId,
                    Status = PropostaStatus.Criada,
                    UsuarioId = request.ResponsavelId,
                    DataCriacao = DateTime.Now
                };

                this.Context.Propostas.Add(obj);

                await this.Context.SaveChangesAsync();

                response.Item = obj;
            }
            catch (BusinessException ex)
            {
                response.Code = ResponseCode.Error;
                response.ResponseStatus.AddError(ex.FieldName, ex.Message);
            }
            catch (Exception ex)
            {
                response.Code = ResponseCode.Fatal;
                response.ResponseStatus.StackTrace = ex.StackTrace;
                response.ResponseStatus.AddError("Erro fatal aconteceu", ex.Message);
            }

            return response;
        }

        public Task<Response> CancelProposta(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Proposta> GetProposta(int id)
        {
            return await this.Context.Propostas.Where(x => x.PropostaId == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Proposta>> GetPropostas(int limit, int offset)
        {
            var query = this.Context.Propostas.AsQueryable();

            query = query.OrderBy(p => p.DataCriacao).Skip(offset).Take(limit);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Proposta>> GetPropostasPorCliente(int id)
        {
            return await this.Context.Propostas.Where(x => x.ClienteId == id).ToListAsync();
        }

        public Task<int> UpdateProposta()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<Proposta>> ValidateProposta(int id)
        {
            var response = new Response<Proposta>();

            var proposta = await this.GetProposta(id);

            response.Item = proposta;

            if (proposta == null)
                response.ResponseStatus.AddError("Proposta", "A proposta não existe no banco de dados");

            if (CpfValidator.Validate(proposta.Cliente.CPF))
                response.ResponseStatus.AddError("Cliente", "O CPF do Cliente nao é válido");

            if (CpfValidator.CheckIrregular(proposta.Cliente.CPF))
            {
                response.ResponseStatus.AddError("CpfIrregular", "O CPF do Cliente está irregular");
                proposta.Status = PropostaStatus.PendenciaAutomatica;
            }
            return response;
        }
    }
}
