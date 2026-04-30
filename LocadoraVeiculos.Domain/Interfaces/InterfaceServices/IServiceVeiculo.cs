using LocadoraVeiculos.Application.DTOs;

namespace LocadoraVeiculos.Domain.Interfaces.InterfaceServices;

public interface IServiceVeiculo
{
    public Task<bool> AdicionarVeiculo(RequestAdicionarVeiculoDTO veiculoDto);

    public Task<bool> EditarVeiculo(Guid id, RequestEditarVeiculoDTO veiculoDto);
}