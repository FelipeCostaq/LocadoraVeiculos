using LocadoraVeiculos.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LocadoraVeiculos.Entities.DTOs
{
    public class RequestAdicionarVeiculoAlocadoDTO
    {
        public Guid ClienteId { get; set; }

        public string PlacaVeiculo { get; set; } = string.Empty;

        public DateTime DataRetirada { get; set; }

        public DateTime DataPrevDevol { get; set; }

        public DateTime DataDevolução { get; set; }
    }
}
