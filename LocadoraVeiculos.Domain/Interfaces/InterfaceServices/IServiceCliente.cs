using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Entities.Entities;

namespace LocadoraVeiculos.Domain.Interfaces.InterfaceServices;

public interface IServiceCliente
{
    public Task<bool> AdicionarCliente(RequestAdicionarClienteDTO clienteDto);

    public Task<bool> EditarCliente(Guid id, RequestEditarClienteDTO clienteDto);
}