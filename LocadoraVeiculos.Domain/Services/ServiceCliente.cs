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
        int idadeCliente = DateTime.Now.Year - clienteDto.DataNasc.Year;
        
        if (idadeCliente >= 18)
        {
            await _cliente.AdicionarCliente(clienteDto);
            return true;
        }

        return false;
    }

    public async Task<bool> EditarCliente(Guid id, RequestEditarClienteDTO clienteDto)
    {
        int idadeCliente = DateTime.Now.Year - clienteDto.DataNasc.Year;
        
        if (idadeCliente >= 18)
        {
            await _cliente.EditarCliente(id, clienteDto);

            return true;
        }

        return false;
    }
}