using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Domain.Interfaces.InterfaceServices;
using LocadoraVeiculos.Domain.Interfaces.InterfaceVeiculo;

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

    public async Task<bool> EditarVeiculo(Guid id, RequestEditarVeiculoDTO veiculoDto)
    {
        if (veiculoDto.Ativo == false || (veiculoDto.Ano >= 1990 && veiculoDto.Ano <= DateTime.Now.Year))
        {
            await _iveiculo.EditarVeiculo(id, veiculoDto);
            
            return true;
        }
        
        return false;
    }
}