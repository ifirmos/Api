using Microsoft.AspNetCore.Mvc;
using Servidor.Data;
using Servidor.Services;

namespace Servidor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentoController : ControllerBase
    {
        private readonly IContratoDocumentoService _contratoDocumentoService;        
        private readonly ILogger<DocumentoController> _logger;
        private readonly AppDbContext _context;

        public DocumentoController(IContratoDocumentoService contratoDocumentoService, AppDbContext context, ILogger<DocumentoController> logger)
        {
            _contratoDocumentoService = contratoDocumentoService;
            _context = context;
            _logger = logger;
        }

        [HttpPost("upload/{idContrato}")]
        public async Task<IActionResult> UploadDocumento(int idContrato, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Arquivo inválido");
            }

            try
            {
                // Chama o serviço para fazer o upload do documento
                var documento = await _contratoDocumentoService.UploadDocumento(idContrato, file);

                // Verifica se o upload foi bem sucedido
                if (documento == null)
                {
                    return BadRequest("Falha ao fazer upload do arquivo");
                }

                // Retorna sucesso
                return Ok(documento);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao fazer upload do documento para o contrato com ID: {Id}", idContrato);
                return BadRequest($"Erro ao fazer upload do documento para o contrato com ID: {idContrato}. Por favor, tente novamente mais tarde.");
            }
        }



        [HttpGet("{idContrato}")]
        public async Task<IActionResult> Download(int idContrato)
        {
            try
            {
                Documento documento = await _contratoDocumentoService.GetDocumentoByContratoId(idContrato);

                if (documento == null || !System.IO.File.Exists(documento.PdfPath))
                {
                    return NotFound(new { message = "Documento não encontrado." });
                }

                var memory = await _contratoDocumentoService.GetDocumentoStream(documento.PdfPath);
                return File(memory, "application/pdf", Path.GetFileName(documento.PdfPath));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao fazer download do documento.");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{idContrato}")]
        public async Task<IActionResult> RemoverDocumento(int idContrato)
        {
            try
            {
                await _contratoDocumentoService.RemoverDocumento(idContrato);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover o documento para o contrato {idContrato}", idContrato);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao remover o documento." });
            }
        }

    }
}