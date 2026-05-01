using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Domain.Interfaces.Generics;
using LocadoraVeiculos.Entities.Entities;

namespace LocadoraVeiculos.Domain.Interfaces.InterfaceCliente;

public interface ICliente : IGenerics<Cliente>
{
    public Task AdicionarCliente(RequestAdicionarClienteDTO clienteDto);

    public Task EditarCliente(Guid id, RequestEditarClienteDTO clienteDto);

    public Task ExcluirCliente(Guid id);

    public Task<bool> ClienteAlocacaoAtiva(Guid id);

    public Task<Cliente> ListarClientePorId(Guid id);

    public Task<List<Cliente>> ListarClientes();
}