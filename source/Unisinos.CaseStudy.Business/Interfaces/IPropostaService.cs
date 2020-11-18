using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Unisinos.CaseStudy.API.Shared;
using Unisinos.CaseStudy.Data.Models;
using Unisinos.CaseStudy.Shared.Requests;

namespace Unisinos.CaseStudy.Business.Interfaces
{
    public interface IPropostaService
    {
        Task<Proposta> GetProposta(int id);
        Task<IEnumerable<Proposta>> GetPropostas(int limit = 100, int offset = 0);
        Task<IEnumerable<Proposta>> GetPropostasPorCliente(int id);
        Task<Response<Proposta>> AddProposta(PropostaRequest request);
        Task<int> UpdateProposta();
        Task<Response<Proposta>> ValidateProposta(int id);
        Task<Response> CancelProposta(int id);
    }
}
