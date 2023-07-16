using Servidor.DTOs;
using Servidor.Models;
using Microsoft.EntityFrameworkCore;
using Servidor.Data;

namespace Servidor.Services
{
    public class ContratoDocumentoService : IContratoDocumentoService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ContratoDocumentoService> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly string _root;

        public ContratoDocumentoService(AppDbContext context, ILogger<ContratoDocumentoService> logger, IWebHostEnvironment environment)
        {
            _context = context;
            _logger = logger;
            _environment = environment;
            _root = Path.Combine(_environment.ContentRootPath, "Data", "UploadsContratos");
        }

        public async Task<List<Contrato>> GetContratos()
        {
            return await _context.Contratos
                .Include(c => c.Documento)  // Carrega os dados de Documento
                .ToListAsync();
        }

        public async Task<Contrato> GetContrato(int id)
        {
            return await _context.Contratos
                .Include(c => c.Documento)  // Carrega os dados de Documento
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Contrato> CreateContrato(ContratoCreateDto contratoCreateDto)
        {
            var contrato = new Contrato
            {
                Cliente = contratoCreateDto.Cliente,
                NumeroContrato = contratoCreateDto.NumeroContrato,
                Valor = contratoCreateDto.Valor,
                DataAssinatura = contratoCreateDto.DataAssinatura
            };

            _context.Contratos.Add(contrato);
            await _context.SaveChangesAsync();

            return contrato;
        }

        public async Task UpdateContrato(int id, ContratoUpdateDto contratoUpdateDto)
        {
            var contrato = await _context.Contratos.FindAsync(id);

            if (contrato != null)
            {
                contrato.Cliente = contratoUpdateDto.Cliente;
                contrato.NumeroContrato = contratoUpdateDto.NumeroContrato;
                contrato.Valor = contratoUpdateDto.Valor;
                contrato.DataAssinatura = contratoUpdateDto.DataAssinatura;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Contrato com ID {id} não encontrado");
            }
        }

        public async Task DeleteContrato(int id)
        {
            // Primeiro, obtemos o contrato do banco de dados
            var contrato = await GetContrato(id);

            // Se o contrato não existir, retornamos diretamente
            if (contrato == null)
            {
                throw new ArgumentException($"Contrato com ID {id} não encontrado.");
            }

            // Removemos o documento associado ao contrato, se houver um
            if (contrato.DocumentoId != null)
            {
                await RemoverDocumento(id);
            }

            // Removemos o contrato em si
            _context.Contratos.Remove(contrato);
            await _context.SaveChangesAsync();

            // Log
            _logger.LogInformation($"Contrato {id} removido com sucesso");
        }


        public async Task<bool> ContratoExists(int id)
        {
            return await _context.Contratos.AnyAsync(e => e.Id == id);
        }

        public Task UpdateContrato(int idContrato, Contrato contrato)
        {
            throw new NotImplementedException();
        }

        private string GetContractUploadsPath(int contratoId)
        {
            // Cria o caminho para o diretório específico do contrato
            string contractUploadsPath = Path.Combine(_root, contratoId.ToString());

            // Garante que o diretório existe
            if (!Directory.Exists(contractUploadsPath))
            {
                Directory.CreateDirectory(contractUploadsPath);
            }

            return contractUploadsPath;
        }        

        public async Task SaveDocumento(Documento documento)
        {
            _context.Documentos.Add(documento);
            await _context.SaveChangesAsync();
        }

        public async Task<Documento> GetDocumentoByContratoId(int idContrato)
        {
            var documento = await _context.Documentos.FirstOrDefaultAsync(d => d.ContratoId == idContrato);
            return documento;
        }

        public async Task<Stream> GetDocumentoStream(string pdfPath)
        {
            var memory = new MemoryStream();

            using (var stream = new FileStream(pdfPath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;

            return memory;
        }

        public async Task<bool> TemDocumentoFisico(int idContrato)
        {
            var contractUploadsPath = GetContractUploadsPath(idContrato);
            var files = Directory.GetFiles(contractUploadsPath);

            return files.Length > 0;
        }

        public async Task<Documento> UploadDocumento(int idContrato, IFormFile file)
        {
            var contrato = await _context.Contratos.FindAsync(idContrato);

            if (contrato == null)
            {
                throw new ArgumentException("Contrato Inexistente");
            }

            var contractUploadsPath = GetContractUploadsPath(idContrato);
            var fileName = $"{idContrato}_{file.FileName}";
            var fullPath = Path.Combine(contractUploadsPath, fileName);

            var documento = await _context.Documentos.FirstOrDefaultAsync(d => d.ContratoId == idContrato);
            bool isNewDocumento = documento == null;

            // Inicie uma transação
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (!isNewDocumento)
                    {
                        // Se o arquivo existir, ele será excluído antes de atualizar as informações
                        if (File.Exists(documento.PdfPath))
                        {
                            File.Delete(documento.PdfPath);
                        }

                        documento.Nome = fileName;
                        documento.PdfPath = fullPath;
                    }
                    else
                    {
                        documento = new Documento
                        {
                            Nome = fileName,
                            PdfPath = fullPath,
                            ContratoId = idContrato
                        };
                        _context.Documentos.Add(documento);
                    }

                    await _context.SaveChangesAsync();

                    if (!Directory.Exists(contractUploadsPath))
                    {
                        Directory.CreateDirectory(contractUploadsPath);
                    }

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    contrato.Documento = documento;
                    contrato.DocumentoId = documento.Id;

                    _context.Contratos.Update(contrato);
                    await _context.SaveChangesAsync();

                    // Se todas as operações forem bem-sucedidas, confirme a transação
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    // Se qualquer operação falhar, reverta a transação
                    await transaction.RollbackAsync();
                    throw;  // re-throw the exception
                }
            }

            return documento;
        }


        public async Task RemoverDocumento(int idContrato)
        {
            var documento = await _context.Documentos.FirstOrDefaultAsync(d => d.ContratoId == idContrato);

            if (documento != null)
            {
                _context.Documentos.Remove(documento);
                await _context.SaveChangesAsync();

                var contractUploadsPath = GetContractUploadsPath(idContrato);

                if (Directory.Exists(contractUploadsPath))
                {
                    Directory.Delete(contractUploadsPath, true);
                }
            }
        }
        string IContratoDocumentoService.GetContractUploadsPath(int idContrato)
        {
            throw new NotImplementedException();
        }
    }
}
