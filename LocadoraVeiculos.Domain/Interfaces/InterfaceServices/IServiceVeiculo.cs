using LocadoraVeiculos.Application.DTOs;

namespace LocadoraVeiculos.Domain.Interfaces.InterfaceServices;

public interface IServiceVeiculo
{
    public Task AdicionarVeiculo(RequestAdicionarVeiculoDTO veiculoDto);

    public Task EditarVeiculo(string placa, RequestEditarVeiculoDTO veiculoDto);
}