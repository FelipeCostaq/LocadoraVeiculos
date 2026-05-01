using LocadoraVeiculos.Application.DTOs;

namespace LocadoraVeiculos.Domain.Interfaces.InterfaceServices;

public interface IServiceCategoriaVeiculo
{
    public Task AdicionarCategoriaVeiculo(RequestAdicionarCategoriaVeiculoDTO categoriaVeiculoDto);

    public Task EditarCategoriaVeiculo(Guid id, RequestEditarCategoriaVeiculoDTO categoriaVeiculoDto);
}