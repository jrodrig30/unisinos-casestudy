using System.Threading.Tasks;
using Unisinos.CaseStudy.API.Shared;
using Unisinos.CaseStudy.Data.Models;
using Unisinos.CaseStudy.Shared.Requests;

namespace Unisinos.CaseStudy.Business.Services
{
    public interface IClienteService
    {
        Task<Response<Cliente>> AddCliente(ClienteRequest request);
        Task<Response<Cliente>> UpdateCliente(ClienteRequest request);
        Task<Response<bool>> DeactivateCliente(int id);
        Task<Response<Cliente>> InactivateCliente(int id);
    }
}
