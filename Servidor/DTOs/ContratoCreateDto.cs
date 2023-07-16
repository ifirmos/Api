using System.ComponentModel.DataAnnotations;

namespace Servidor.DTOs
{
    public class ContratoCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Cliente { get; set; }

        [Required]
        [StringLength(20)]
        public string NumeroContrato { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Valor { get; set; }

        [Required]
        public DateTime DataAssinatura { get; set; }
    }
}
