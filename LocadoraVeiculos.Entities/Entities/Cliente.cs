using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.Entities.Entities
{
    public class Cliente
    {
        // PK
        public Guid Id { get; set; } = Guid.CreateVersion7();

        [Required]
        [MaxLength(150)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string CPF { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Telefone { get; set; } = string.Empty;

        public DateOnly DataNasc { get; set; } 

        public string? Endereco { get; set; } = string.Empty;

        public bool Ativo { get; set; } = true;

        public DateTime CriadoEm { get; set; } = DateTime.Now;
    }
}
