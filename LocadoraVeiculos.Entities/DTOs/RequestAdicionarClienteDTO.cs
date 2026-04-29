namespace LocadoraVeiculos.Application.DTOs;

public class RequestAdicionarClienteDTO
{
    public string Nome { get; set; } = string.Empty;

    public string CPF { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;

    public string Telefone { get; set; } = string.Empty;

    public DateOnly DataNasc { get; set; }

    public string? Endereco { get; set; } = string.Empty;
}