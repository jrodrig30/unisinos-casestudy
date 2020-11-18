using System.Threading.Tasks;
using Unisinos.CaseStudy.API.Shared;
using Unisinos.CaseStudy.Data.Models;
using Unisinos.CaseStudy.Shared.Requests;

namespace Unisinos.CaseStudy.Business.Services
{
    public interface IDocumentoService
    {
        Task<Response> DeleteDocumento(int id);
        Task<Response> UploadDocumento();
        Task<bool> ValidateDocumentos(int id);
    }
}
