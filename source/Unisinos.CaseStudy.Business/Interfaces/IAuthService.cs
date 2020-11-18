using System;
using System.Threading.Tasks;
using Unisinos.CaseStudy.API.Shared;
using Unisinos.CaseStudy.Data.Models;
using Unisinos.CaseStudy.Shared.Requests;

namespace Unisinos.CaseStudy.Business.Services
{
    public interface IAuthService
    {
        Task<Response<Guid>> Authenticate(LoginRequest request);

        Task<Response<bool>> Register(UserRegisterRequest request);

        Task<Response<bool>> ForgetPassword(int id);
    }
}
