using LocadoraVeiculos.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LocadoraVeiculos.Entities.Entities
{
    public class VeiculoAlocado
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();

        [ForeignKey("Cliente")]
        [Required]
        public Guid ClienteId { get; set; }

        [ForeignKey("Veiculo")]
        [Required]
        public string PlacaVeiculo { get; set; } = string.Empty;

        [Required]
        public DateTime DataRetirada { get; set; }

        [Required]
        public DateTime DataPrevDevol { get; set; }

        [Required]
        public DateTime DataDevolução { get; set; }

        [Required]
        public decimal ValorTotal { get; set; }

        [Required]
        public Status Status { get; set; } = Status.Ativa;

        [Required]
        public DateTime CriadoEm { get; set; } = DateTime.Now;
    }
}
