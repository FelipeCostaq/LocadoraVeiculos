using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Domain.Interfaces.InterfaceCategoriaVeiculo;
using LocadoraVeiculos.Domain.Interfaces.InterfaceServices;

namespace LocadoraVeiculos.Domain.Services;

public class ServiceCategoriaVeiculo : IServiceCategoriaVeiculo
{
    private readonly ICategoriaVeiculo _icategoriaVeiculo;

    public ServiceCategoriaVeiculo(ICategoriaVeiculo icategoriaVeiculo)
    {
        _icategoriaVeiculo = icategoriaVeiculo;
    }
    
    public async Task<bool> AdicionarCategoriaVeiculo(RequestAdicionarCategoriaVeiculoDTO categoriaVeiculoDto)
    {
        if (categoriaVeiculoDto.ValorDiaria >= 1)
        {
            await _icategoriaVeiculo.AdicionarCategoriaVeiculo(categoriaVeiculoDto);
            
            return true;
        }

        return false;
    }

    public async Task<bool> EditarCategoriaVeiculo(Guid id, RequestEditarCategoriaVeiculoDTO categoriaVeiculoDto)
    {
        if (categoriaVeiculoDto.ValorDiaria >= 1)
        {
            await _icategoriaVeiculo.EditarCategoriaVeiculo(id, categoriaVeiculoDto);
            
            return true;
        }

        return false;
    }
}