using Servidor.Models;
using System.Text.Json.Serialization;


public class Documento
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string PdfPath { get; set; }
    public int ContratoId { get; set; }
    [JsonIgnore] //anotação para evitar a serialização circular. Caso contrário, é gerado o arquivo e a pasta, mas o documento não é salvo no banco de dados. 
    public Contrato Contrato { get; set; }
}
