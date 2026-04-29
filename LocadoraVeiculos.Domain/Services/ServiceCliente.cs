using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Domain.Interfaces.InterfaceCliente;
using LocadoraVeiculos.Domain.Interfaces.InterfaceServices;
using LocadoraVeiculos.Entities.Entities;

namespace LocadoraVeiculos.Domain.Services;

public class ServiceCliente : IServiceCliente
{
    private readonly ICliente _cliente;

    public ServiceCliente(ICliente cliente)
    {
        _cliente = cliente;
    }
    
    public async Task<bool> AdicionarCliente(RequestAdicionarClienteDTO clienteDto)
    {
        if (DateTime.Now.Year - clienteDto.DataNasc.Year >= 18)
        {
            await _cliente.AdicionarCliente(clienteDto);
            return true;
        }

        return false;
    }

    public async Task<bool> EditarCliente(Guid id, RequestEditarClienteDTO clienteDto)
    {
        if (DateTime.Now.Year - clienteDto.DataNasc.Year >= 18 && clienteDto.Ativo)
        {
            await _cliente.EditarCliente(id, clienteDto);

            return true;
        }

        return false;
    }

    public async Task<Cliente> ListarClientePorId(Guid id)
    {
        var cliente = await _cliente.ListarClientePorId(id);

        return cliente;
    }

    public async Task<List<Cliente>> ListarClientes()
    {
        var clientes = await _cliente.ListarClientes();

        return clientes;
    }
}