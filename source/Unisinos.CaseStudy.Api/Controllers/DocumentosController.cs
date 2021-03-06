﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unisinos.CaseStudy.API.Shared;
using Unisinos.CaseStudy.Business.Interfaces;
using Unisinos.CaseStudy.Business.Services;
using Unisinos.CaseStudy.Data.Models;
using Unisinos.CaseStudy.Shared.Requests;

namespace Unisinos.CaseStudy.Controllers
{
    [Route("api/documentos")]
    [Produces("application/json")]
    [ApiController]
    public class DocumentosController : ControllerBase
    {
        private readonly IClienteService _service;

        // Dependências são injetadas
        public DocumentosController(IClienteService service)
        {
            this._service = service;
        }
     }
}
