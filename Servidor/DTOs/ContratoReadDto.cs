namespace Servidor.DTOs
{
    public class ContratoReadDto
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public string NumeroContrato { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataAssinatura { get; set; }
        public bool TemPDF { get; set; }
    }
}


