using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Entities.Entities;

namespace LocadoraVeiculos.Domain.Interfaces.InterfaceServices;

public interface IServiceCliente
{
    public Task AdicionarCliente(RequestAdicionarClienteDTO clienteDto);

    public Task EditarCliente(Guid id, RequestEditarClienteDTO clienteDto);

    public Task ExcluirCliente(Guid id);
}