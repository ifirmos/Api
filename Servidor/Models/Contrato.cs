using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Servidor.Models
{
    public class Contrato
    {
        public int Id { get; set; }

        [Required]
        public string Cliente { get; set; }

        [Required]
        public string NumeroContrato { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Valor { get; set; }

        [Required]
        public DateTime DataAssinatura { get; set; }

        public int? DocumentoId { get; set; }

        [ForeignKey("DocumentoId")]
        public Documento? Documento { get; set; }
    }
}