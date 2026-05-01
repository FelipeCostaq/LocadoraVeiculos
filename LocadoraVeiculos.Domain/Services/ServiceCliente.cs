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
    
    public async Task AdicionarCliente(RequestAdicionarClienteDTO clienteDto)
    {
        int idadeCliente = DateTime.Now.Year - clienteDto.DataNasc.Year;
        
        if (idadeCliente < 18)
            throw new Exception("O cliente deve ter pelo menos 18 anos para ser cadastrado.");

        await _cliente.AdicionarCliente(clienteDto);
    }

    public async Task<bool> ClienteAlocacaoAtiva(Guid id)
    {
        return await _cliente.ClienteAlocacaoAtiva(id);
    }

    public async Task EditarCliente(Guid id, RequestEditarClienteDTO clienteDto)
    {
        int idadeCliente = DateTime.Now.Year - clienteDto.DataNasc.Year;

        if (idadeCliente < 18)
            throw new Exception("O cliente deve ter pelo menos 18 anos para ser cadastrado.");

        await _cliente.EditarCliente(id, clienteDto);
    }

    public async Task ExcluirCliente(Guid id)
    {
        var cliente = await _cliente.ListarClientePorId(id);

        if (await _cliente.ClienteAlocacaoAtiva(id))
            throw new Exception("O Cliente está com uma alocação ativa.");

        await _cliente.ExcluirCliente(id);
    }
}