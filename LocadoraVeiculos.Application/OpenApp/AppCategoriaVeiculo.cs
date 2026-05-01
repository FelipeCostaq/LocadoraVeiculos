using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Application.Interfaces;
using LocadoraVeiculos.Domain.Interfaces.InterfaceCategoriaVeiculo;
using LocadoraVeiculos.Domain.Interfaces.InterfaceServices;
using LocadoraVeiculos.Entities.Entities;

namespace LocadoraVeiculos.Application.OpenApp;

public class AppCategoriaVeiculo : InterfaceCategoriaVeiculoApp
{
    private readonly ICategoriaVeiculo _icategoriaVeiculo;
    private readonly IServiceCategoriaVeiculo _iserviceCategoriaVeiculo;

    public AppCategoriaVeiculo(ICategoriaVeiculo icategoriaVeiculo, IServiceCategoriaVeiculo iserviceCategoriaVeiculo )
    {
        _icategoriaVeiculo = icategoriaVeiculo;
        _iserviceCategoriaVeiculo = iserviceCategoriaVeiculo;
    }
    
    public async Task AdicionarCategoriaVeiculo(RequestAdicionarCategoriaVeiculoDTO categoriaVeiculoDto)
    {
        await _iserviceCategoriaVeiculo.AdicionarCategoriaVeiculo(categoriaVeiculoDto);
    }

    public async Task<bool> CategoriaEmUso(Guid id)
    {
        return await _icategoriaVeiculo.CategoriaEmUso(id);
    }

    public async Task EditarCategoriaVeiculo(Guid id, RequestEditarCategoriaVeiculoDTO categoriaVeiculoDto)
    {
        await _icategoriaVeiculo.EditarCategoriaVeiculo(id, categoriaVeiculoDto);
    }

    public async Task<List<CategoriaVeiculo>> ListarCategoriasVeiculo()
    {
        return await _icategoriaVeiculo.ListarCategoriasVeiculo();
    }

    public async Task<CategoriaVeiculo> ListarCategoriasVeiculoPorId(Guid id)
    {
        return await _icategoriaVeiculo.ListarCategoriasVeiculoPorId(id);
    }
}