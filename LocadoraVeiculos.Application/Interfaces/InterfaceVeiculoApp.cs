using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Entities.Entities;

namespace LocadoraVeiculos.Application.Interfaces;

public interface InterfaceVeiculoApp
{
    Task AdicionarVeiculo(RequestAdicionarVeiculoDTO veiculoDto);
    Task EditarVeiculo(string placa, RequestEditarVeiculoDTO veiculoDto);
    Task<List<Veiculo>> ListarVeiculos();
    Task<List<Veiculo>> ListarVeiculosDisponiveis();
}