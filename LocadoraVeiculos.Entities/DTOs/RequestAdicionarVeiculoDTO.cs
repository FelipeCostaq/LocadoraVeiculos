namespace LocadoraVeiculos.Application.DTOs;

public class RequestAdicionarVeiculoDTO
{
    public string Placa { get; set; } = string.Empty;
    
    public string Marca { get; set; } = string.Empty;
    
    public string Modelo { get; set; } = string.Empty;
    
    public int Ano { get; set; }
    
    public string Cor { get; set; } = string.Empty;

    public Guid CategoriaId { get; set; } = Guid.Empty;

    public string? ImagemUrl { get; set; } = string.Empty;

    public bool Disponivel { get; set; }
}