using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LocadoraVeiculos.Entities.Entities
{
    public class CategoriaVeiculo
    {
        // PK
        public Guid Id { get; set; } = Guid.Empty;

        [Required]
        public string Nome { get; set; } = string.Empty;

        public string? Descricao { get; set; } = string.Empty;

        [Required]
        public decimal ValorDiaria { get; set; }

        public bool Ativo { get; set; } = true;
    }
}
