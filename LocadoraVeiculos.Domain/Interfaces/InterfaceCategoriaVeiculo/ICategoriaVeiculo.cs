using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Domain.Interfaces.Generics;
using LocadoraVeiculos.Entities.Entities;

namespace LocadoraVeiculos.Domain.Interfaces.InterfaceCategoriaVeiculo;

public interface ICategoriaVeiculo : IGenerics<CategoriaVeiculo>
{
    public Task AdicionarCategoriaVeiculo(RequestAdicionarCategoriaVeiculoDTO categoriaVeiculoDto);

    public Task EditarCategoriaVeiculo(Guid id, RequestEditarCategoriaVeiculoDTO categoriaVeiculoDto);

    public Task<List<CategoriaVeiculo>> ListarCategoriasVeiculo();
}