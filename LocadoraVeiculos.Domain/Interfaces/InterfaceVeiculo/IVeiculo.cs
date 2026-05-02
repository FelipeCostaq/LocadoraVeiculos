using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Entities.Entities;

namespace LocadoraVeiculos.Domain.Interfaces.InterfaceVeiculo;

public interface IVeiculo
{
    public Task AdicionarVeiculo(RequestAdicionarVeiculoDTO veiculoDto);

    public Task EditarVeiculo(string placa, RequestEditarVeiculoDTO veiculoDto);

    public Task<List<Veiculo>> ListarVeiculos();

    public Task<List<Veiculo>> ListarVeiculosDisponiveis();

    public Task<Veiculo> ListarVeiculoPorId(string placa);

    public Task<bool> VeiculoLocacaoAtivo(string placa);
    public Task<bool> VeiculoCategoriaAtivo(Guid id);
}