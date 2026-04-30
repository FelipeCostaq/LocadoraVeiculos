namespace LocadoraVeiculos.Application.DTOs;

public class RequestAdicionarCategoriaVeiculoDTO
{
    public string Nome { get; set; } = string.Empty;

    public string? Descricao { get; set; } = string.Empty;
    
    public decimal ValorDiaria { get; set; }
}