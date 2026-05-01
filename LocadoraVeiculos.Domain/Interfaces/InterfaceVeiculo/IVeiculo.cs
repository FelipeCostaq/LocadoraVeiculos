using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Entities.Entities;

namespace LocadoraVeiculos.Domain.Interfaces.InterfaceVeiculo;

public interface IVeiculo
{
    public Task AdicionarVeiculo(RequestAdicionarVeiculoDTO veiculoDto);

    public Task EditarVeiculo(string placa, RequestEditarVeiculoDTO veiculoDto);

    public Task<List<Veiculo>> ListarVeiculos();

    public Task<List<Veiculo>> ListarVeiculosDisponiveis();
}