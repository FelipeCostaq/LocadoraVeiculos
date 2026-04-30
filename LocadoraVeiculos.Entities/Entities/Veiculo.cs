using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LocadoraVeiculos.Entities.Entities
{
    public class Veiculo
    {
        [Key]
        public string Placa { get; set; } = string.Empty;

        [Required]
        public string Marca { get; set; } = string.Empty;

        [Required]
        public string Modelo { get; set; } = string.Empty;

        [Required]
        public int Ano { get; set; }

        [Required]
        public string Cor { get; set; } = string.Empty;

        [ForeignKey("CategoriaVeiculo")]
        [Required]
        public Guid CategoriaId { get; set; } = Guid.Empty;

        public string? ImagemUrl { get; set; } = string.Empty;

        public bool Disponivel { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
