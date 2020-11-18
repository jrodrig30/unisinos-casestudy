using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Unisinos.CaseStudy.API.Shared;
using Unisinos.CaseStudy.Data;
using Unisinos.CaseStudy.Data.Models;
using Unisinos.CaseStudy.Shared.Exceptions;
using Unisinos.CaseStudy.Shared.Requests;

namespace Unisinos.CaseStudy.Business.Services
{
    public class ClienteService : BaseService, IClienteService
    {
        public ClienteService(BemPromotoraContext context) : base(context) { }

        public async Task<Response<Cliente>> AddCliente(ClienteRequest request)
        {
            var response = new Response<Cliente>();

            try
            {
                var cliente = await this.Context.Clientes.FirstOrDefaultAsync(x => x.CPF == request.CPF);

                if (cliente != null)
                    throw new BusinessException("ClienteJaExiste", "Já existe um cliente com este CPF cadastrado no banco de dados.");

                var obj = new Cliente
                {
                    CPF = request.CPF,
                    Nome = request.Nome,
                    DataNascimento = request.DataNascimento,
                    Sexo = request.Sexo,
                    Email = request.Email
                };

                this.Context.Clientes.Add(obj);

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

        public Task<Response<bool>> DeactivateCliente(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Cliente>> InactivateCliente(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Cliente>> UpdateCliente(ClienteRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
