using System.Text.RegularExpressions;
using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Domain.Interfaces.InterfaceServices;
using LocadoraVeiculos.Domain.Interfaces.InterfaceVeiculo;
using LocadoraVeiculos.Entities.Entities;

namespace LocadoraVeiculos.Domain.Services;

public class ServiceVeiculo : IServiceVeiculo
{
    private readonly IVeiculo _iveiculo;

    public ServiceVeiculo(IVeiculo iveiculo)
    {
        _iveiculo = iveiculo;
    }
    
    public async Task AdicionarVeiculo(RequestAdicionarVeiculoDTO veiculoDto)
    {
        if (!(veiculoDto.Ano >= 1990) && !(veiculoDto.Ano <= DateTime.Now.Year))
            throw new InvalidOperationException($"O ano do veículo deve estar entre 1990 e {DateTime.Now.Year}");
        
        if (!ValidarPlaca(veiculoDto.Placa))
            throw new InvalidOperationException("Placa inválida");
        
        await _iveiculo.AdicionarVeiculo(veiculoDto);
    }

    public async Task EditarVeiculo(string placa, RequestEditarVeiculoDTO veiculoDto)
    {
        Veiculo veiculo = await _iveiculo.ListarVeiculoPorId(placa);

        if (veiculoDto.Ativo == false && await _iveiculo.VeiculoLocacaoAtivo(placa))
            throw new InvalidOperationException("O status ativo do veículo não pode ser alterado enquanto ele está alocado.");

        // Caso o veículo seja desativado ele também fica indisponível.
        if (veiculoDto.Ativo == false)
            veiculoDto.Disponivel = false;

        if (veiculoDto.CategoriaId != veiculo.CategoriaId && await _iveiculo.VeiculoLocacaoAtivo(placa))
            throw new InvalidOperationException("A categoria do veículo não pode ser alterado enquanto ele está alocado.");
        
        if (veiculoDto.Disponivel != veiculo.Disponivel && await _iveiculo.VeiculoLocacaoAtivo(placa))
            throw new InvalidOperationException("O status disponível do veículo não pode ser alterado enquanto ele está alocado.");

        if (!(veiculoDto.Ano >= 1990 && veiculoDto.Ano <= DateTime.Now.Year))
            throw new InvalidOperationException($"O ano do veículo deve estar entre 1990 e {DateTime.Now.Year}");

        await _iveiculo.EditarVeiculo(placa, veiculoDto);
    }

    public static bool ValidarPlaca(string placa)
    {
        if (string.IsNullOrWhiteSpace(placa)) return false;
        
        string placaLimpa = placa.Replace("-", "").Replace(" ", "").ToUpper();
        
        if (placaLimpa.Length != 7) return false;
        
        const string padraoAntigo = @"^[A-Z]{3}[0-9]{4}$";
        
        const string padraoMercosul = @"^[A-Z]{3}[0-9][A-Z][0-9]{2}$";
        
        return Regex.IsMatch(placaLimpa, padraoAntigo, RegexOptions.Compiled) || 
               Regex.IsMatch(placaLimpa, padraoMercosul, RegexOptions.Compiled);
    }
}