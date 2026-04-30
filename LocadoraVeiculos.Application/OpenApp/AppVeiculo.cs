using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Application.Interfaces;
using LocadoraVeiculos.Domain.Interfaces.InterfaceServices;
using LocadoraVeiculos.Domain.Interfaces.InterfaceVeiculo;
using LocadoraVeiculos.Entities.Entities;

namespace LocadoraVeiculos.Application.OpenApp;

public class AppVeiculo : InterfaceVeiculoApp
{
    private readonly IVeiculo _iveiculo;
    private readonly IServiceVeiculo _iserviceVeiculo;

    public AppVeiculo(IVeiculo iveiculo, IServiceVeiculo iserviceVeiculo)
    {
        _iveiculo = iveiculo;
        _iserviceVeiculo = iserviceVeiculo;
    }
    
    public async Task AdicionarVeiculo(RequestAdicionarVeiculoDTO veiculoDto)
    {
        await _iserviceVeiculo.AdicionarVeiculo(veiculoDto);
    }

    public async Task EditarVeiculo(Guid id, RequestEditarVeiculoDTO veiculoDto)
    {
        await _iserviceVeiculo.EditarVeiculo(id, veiculoDto);
    }

    public Task<List<Veiculo>> ListarVeiculos()
    {
        return _iveiculo.ListarVeiculos();
    }

    public Task<List<Veiculo>> ListarVeiculosDisponiveis()
    {
        return _iveiculo.ListarVeiculosDisponiveis();
    }
}