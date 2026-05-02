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

        // Verificar alteração do status Disponivel enquanto o veículo está alocado.
        if (veiculoDto.Disponivel != veiculo.Disponivel && await _iveiculo.VeiculoLocacaoAtivo(placa))
            throw new InvalidOperationException("O status disponível do veículo não pode ser alterado enquanto ele está alocado.");

        if (!(veiculoDto.Ano >= 1990 && veiculoDto.Ano <= DateTime.Now.Year))
            throw new InvalidOperationException($"O ano do veículo deve estar entre 1990 e {DateTime.Now.Year}");

        await _iveiculo.EditarVeiculo(placa, veiculoDto);
    }
}