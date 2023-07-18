using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servidor.Data;
using Servidor.DTOs;
using Servidor.Models;
using Servidor.Services;

namespace Servidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratosController : ControllerBase
    {
        private readonly ILogger<ContratosController> _logger;
        private readonly AppDbContext _context;
        private readonly IContratoDocumentoService _contratoDocumentoService;

        public ContratosController(ILogger<ContratosController> logger, AppDbContext context, IContratoDocumentoService contratoDocumentoService)
        {
            _logger = logger;
            _context = context;
            _contratoDocumentoService = contratoDocumentoService;
        }

        // GET: api/Contratos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contrato>>> GetContratos()
        {
            try
            {
                var contratos = await _contratoDocumentoService.GetContratos();
                return Ok(contratos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter todos os contratos");
                return BadRequest("Erro ao obter todos os contratos. Por favor, tente novamente mais tarde.");
            }
        }

        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<IEnumerable<Contrato>>> GetContratoPorNome(string nome)
        {
            var contratos = await _context.Contratos
                .Where(c => c.Cliente.ToLower().Contains(nome.ToLower()))
                .ToListAsync();

            if (!contratos.Any())
            {
                return NotFound();
            }

            return contratos;
        }

        [HttpGet("numero/{numero}")]
        public async Task<ActionResult<IEnumerable<Contrato>>> GetContratoPorNumero(string numero)
        {
            var contratos = await _context.Contratos
                .Where(c => c.NumeroContrato.Contains(numero))
                .ToListAsync();

            if (!contratos.Any())
            {
                return NotFound();
            }

            return contratos;
        }

        // GET: api/Contratos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Contrato>> GetContrato(int id)
        {
            try
            {
                var contrato = await _contratoDocumentoService.GetContrato(id);

                if (contrato == null)
                {
                    return NotFound("Contrato não encontrado");
                }

                return Ok(contrato);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter contrato com ID: {Id}", id);
                return BadRequest($"Erro ao obter contrato com ID: {id}. Por favor, tente novamente mais tarde.");
            }
        }

        // POST: api/Contratos
        [HttpPost]
        public async Task<ActionResult<Contrato>> PostContrato(ContratoCreateDto contratoCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dados do contrato inválidos");
            }

            try
            {
                var contrato = await _contratoDocumentoService.CreateContrato(contratoCreateDto);
                return CreatedAtAction(nameof(GetContrato), new { id = contrato.Id }, contrato);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar contrato");
                return BadRequest("Erro ao criar contrato. Por favor, tente novamente mais tarde.");
            }
        }

        // PUT: api/Contratos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContrato(int id, ContratoUpdateDto contratoUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dados do contrato inválidos");
            }

            try
            {
                await _contratoDocumentoService.UpdateContrato(id, contratoUpdateDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar contrato com ID: {Id}", id);
                return NotFound($"Erro ao atualizar contrato com ID: {id}. Por favor, tente novamente mais tarde.");
            }
        }

        // DELETE: api/Contratos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContrato(int id)
        {
            try
            {
                await _contratoDocumentoService.DeleteContrato(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar contrato com ID: {Id}", id);
                return NotFound($"Erro ao deletar contrato com ID: {id}. Por favor, tente novamente mais tarde.");
            }
        }

    }
}
