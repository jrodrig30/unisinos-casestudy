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
    public class AuthService : BaseService, IAuthService
    {
        public AuthService(BemPromotoraContext context) : base(context) { }

        public Task<Response<Guid>> Authenticate(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> ForgetPassword(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> Register(UserRegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
