using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Servidor.DTOs;
using Servidor.Models;

namespace Servidor.Services
{
    public interface IContratoDocumentoService
    {
        Task<List<Contrato>> GetContratos();
        Task<Contrato> GetContrato(int id);
        Task<Contrato> CreateContrato(ContratoCreateDto contratoCreateDto);
        Task UpdateContrato(int id, ContratoUpdateDto contratoUpdateDto);
        Task DeleteContrato(int id);
        Task<bool> ContratoExists(int idContrato);
        Task<Documento> UploadDocumento(int idContrato, IFormFile file);
        Task SaveDocumento(Documento documento);
        Task<Documento> GetDocumentoByContratoId(int idContrato);
        Task<Stream> GetDocumentoStream(string pdfPath);
        Task<bool> TemDocumentoFisico(int idContrato);
        Task RemoverDocumento(int idContrato);
        string GetContractUploadsPath(int idContrato);
    }
}
