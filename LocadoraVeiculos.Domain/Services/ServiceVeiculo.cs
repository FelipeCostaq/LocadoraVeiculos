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
    
    public async Task<bool> AdicionarVeiculo(RequestAdicionarVeiculoDTO veiculoDto)
    {
        if (veiculoDto.Ano >= 1990 && veiculoDto.Ano <= DateTime.Now.Year)
        {
            await _iveiculo.AdicionarVeiculo(veiculoDto);

            return true;
        }
        
        return false;
    }

    public async Task<bool> EditarVeiculo(string placa, RequestEditarVeiculoDTO veiculoDto)
    {
        Veiculo veiculo = await _iveiculo.ListarVeiculoPorId(placa);

        // Verificar alteração do campo ativo enquanto o veículo está alocado.
        if (veiculoDto.Ativo == false && await _iveiculo.VeiculoLocacaoAtivo(placa))
            return false;

        // Caso o veículo seja desativado ele também fica indisponível.
        if (veiculoDto.Ativo == false)
            veiculoDto.Disponivel = false;

        // Verificar alteração da categoria enquanto o veículo está alocado.
        if (veiculoDto.CategoriaId != veiculo.CategoriaId && await _iveiculo.VeiculoLocacaoAtivo(placa))
            return false;

        // Verificar alteração do status Disponivel enquanto o veículo está alocado.
        if (veiculoDto.Disponivel != veiculo.Disponivel && await _iveiculo.VeiculoLocacaoAtivo(placa))
            return false;

        if (veiculoDto.Ativo == false || (veiculoDto.Ano >= 1990 && veiculoDto.Ano <= DateTime.Now.Year))
        {
            await _iveiculo.EditarVeiculo(placa, veiculoDto);
            
            return true;
        }
        
        return false;
    }
}