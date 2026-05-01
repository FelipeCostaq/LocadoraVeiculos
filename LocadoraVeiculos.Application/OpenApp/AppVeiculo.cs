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

    public async Task EditarVeiculo(string placa, RequestEditarVeiculoDTO veiculoDto)
    {
        await _iserviceVeiculo.EditarVeiculo(placa, veiculoDto);
    }

    public async Task<Veiculo> ListarVeiculoPorId(string placa)
    {
        return await _iveiculo.ListarVeiculoPorId(placa);
    }

    public Task<List<Veiculo>> ListarVeiculos()
    {
        return _iveiculo.ListarVeiculos();
    }

    public Task<List<Veiculo>> ListarVeiculosDisponiveis()
    {
        return _iveiculo.ListarVeiculosDisponiveis();
    }

    public Task<bool> VeiculoLocacaoAtivo(string placa)
    {
        return _iveiculo.VeiculoLocacaoAtivo(placa);
    }
}