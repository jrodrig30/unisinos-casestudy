using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unisinos.CaseStudy.API.Shared;
using Unisinos.CaseStudy.Business.Interfaces;
using Unisinos.CaseStudy.Data.Models;
using Unisinos.CaseStudy.Shared.Requests;

namespace Unisinos.CaseStudy.Controllers
{
    [Route("api/propostas")]
    [Produces("application/json")]
    [ApiController]
    public class PropostasController : ControllerBase
    {
        private readonly IPropostaService _service;

        // Dependências são injetadas
        public PropostasController(IPropostaService service)
        {
            this._service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proposta>>> Get(int limit = 100, int offset = 0)
        {
            if (limit <= 0 || limit > 1000)
            {
                return BadRequest($"Limite deve ser entre 1 e 100");
            }

            if (offset < 0)
            {
                return BadRequest($"Offset deve ser no mínimo 0");
            }

            var items = await _service.GetPropostas(limit, offset);

            return items.ToList();
        }

        [HttpGet, Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Proposta>> Get(int id)
        {
            var item = await _service.GetProposta(id);
            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpGet]
        [Route("clientes/{id}")]
        public async Task<ActionResult<IEnumerable<string>>> GetPropostasPorCliente(int id)
        {
            return Ok(await this._service.GetPropostasPorCliente(id));
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Post([FromBody]PropostaRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest("A Proposta precisa estar preenchida");

                if (!ModelState.IsValid)
                    return BadRequest("Modelo inválido");

                var obj = await _service.AddProposta(request);

                return CreatedAtAction("Get", new { id = obj.Item.PropostaId }, obj);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("{id}/validate")]
        public async Task<ActionResult<Response>> Validate(int id)
        {
            return Ok(await this._service.ValidateProposta(id));
        }
    }
}
