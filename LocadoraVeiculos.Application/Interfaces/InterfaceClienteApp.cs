using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Entities.Entities;

namespace LocadoraVeiculos.Application.Interfaces;

public interface InterfaceClienteApp
{
    Task AdicionarCliente(RequestAdicionarClienteDTO clienteDto);
    Task EditarCliente(Guid id, RequestEditarClienteDTO clienteDto);
    Task ExcluirCliente(Guid id);
    Task<List<Cliente>> ListarClientes();
    Task<Cliente> ListarClientePorId(Guid id);
}