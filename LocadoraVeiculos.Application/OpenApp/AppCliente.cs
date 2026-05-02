using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Application.Interfaces;
using LocadoraVeiculos.Domain.Interfaces.InterfaceCliente;
using LocadoraVeiculos.Domain.Interfaces.InterfaceServices;
using LocadoraVeiculos.Entities.Entities;

namespace LocadoraVeiculos.Application.OpenApp;

public class AppCliente : InterfaceClienteApp
{
    private readonly ICliente _icliente;
    private readonly IServiceCliente _iserviceCliente;

    public AppCliente(ICliente icliente, IServiceCliente iserviceCliente)
    {
        _icliente = icliente;
        _iserviceCliente = iserviceCliente;
    }
    
    
    public async Task AdicionarCliente(RequestAdicionarClienteDTO clienteDto)
    {
        await _iserviceCliente.AdicionarCliente(clienteDto);
    }

    public async Task EditarCliente(Guid id, RequestEditarClienteDTO clienteDto)
    {
        await _iserviceCliente.EditarCliente(id, clienteDto);
    }

    public async Task<List<Cliente>> ListarClientes()
    {
        var clientes = await _icliente.ListarClientes();
        
        return clientes;
    }

    public async Task<Cliente> ListarClientePorId(Guid id)
    {
        var cliente = await _icliente.ListarClientePorId(id);
        
        return cliente;
    }

    public async Task ExcluirCliente(Guid id)
    {
        await _iserviceCliente.ExcluirCliente(id);
    }
}