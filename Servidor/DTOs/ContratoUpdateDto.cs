namespace Servidor.DTOs
{
    public class ContratoUpdateDto
    {
        public string Cliente { get; set; }
        public string NumeroContrato { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataAssinatura { get; set; }
    }
}
