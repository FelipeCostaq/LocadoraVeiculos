using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Entities.Entities;

namespace LocadoraVeiculos.Application.Interfaces;

public interface InterfaceCategoriaVeiculoApp
{
    Task AdicionarCategoriaVeiculo(RequestAdicionarCategoriaVeiculoDTO categoriaVeiculoDto);
    Task EditarCategoriaVeiculo(Guid id, RequestEditarCategoriaVeiculoDTO categoriaVeiculoDto);
    Task<List<CategoriaVeiculo>> ListarCategoriasVeiculo();
}