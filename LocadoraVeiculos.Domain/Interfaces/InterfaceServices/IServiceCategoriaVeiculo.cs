using LocadoraVeiculos.Application.DTOs;

namespace LocadoraVeiculos.Domain.Interfaces.InterfaceServices;

public interface IServiceCategoriaVeiculo
{
    public Task<bool> AdicionarCategoriaVeiculo(RequestAdicionarCategoriaVeiculoDTO categoriaVeiculoDto);

    public Task<bool> EditarCategoriaVeiculo(Guid id, RequestEditarCategoriaVeiculoDTO categoriaVeiculoDto);
}