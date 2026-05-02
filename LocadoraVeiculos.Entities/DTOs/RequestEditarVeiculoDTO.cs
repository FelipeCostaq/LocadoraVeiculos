using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace LocadoraVeiculos.Application.DTOs;

public class RequestEditarVeiculoDTO
{   
    public string Marca { get; set; } = string.Empty;
    
    public string Modelo { get; set; } = string.Empty;
    
    public int Ano { get; set; }
    
    public string Cor { get; set; } = string.Empty;

    public Guid CategoriaId { get; set; } = Guid.Empty;

    public IFormFile? Imagem { get; set; }
    
    [NotMapped]
    public string? ImagemUrl { get; set; }

    public bool Disponivel { get; set; }

    public bool Ativo { get; set; }
}