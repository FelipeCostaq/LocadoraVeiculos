using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Application.Interfaces;
using LocadoraVeiculos.Domain.Services;
using LocadoraVeiculos.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraVeiculos.API.Controllers;

[Authorize]
[ApiController]
[Route("clientes")]
public class ClienteController : ControllerBase
{
    public readonly InterfaceClienteApp _interfaceClienteApp;
    public readonly ServiceCliente _serviceCliente;

    public ClienteController(InterfaceClienteApp interfaceClienteApp, ServiceCliente serviceCliente)
    {
        _interfaceClienteApp = interfaceClienteApp;
        _serviceCliente = serviceCliente;
    }

    [HttpGet]
    public async Task<IActionResult> ListarClientes()
    {
        var clientes = await _interfaceClienteApp.ListarClientes();
        
        return Ok(clientes);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ListarClientePorId(Guid id)
    {
        var cliente = await _interfaceClienteApp.ListarClientePorId(id);
        
        if (cliente == null)
            return NotFound();

        return Ok(cliente);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AdicionarCliente(RequestAdicionarClienteDTO clienteDto)
    {
        try
        {
            var created = await _serviceCliente.AdicionarCliente(clienteDto);
            
            if (created)
            {
                return Created();
            }
        }
        catch (Exception)
        {
            return BadRequest("Os campos CPF e Email devem ser únicos");
        }

        return BadRequest("A idade do cliente deve ser maior que 18 anos.");
    }
    
    [HttpPut("edit")]
    public async Task<IActionResult> EditarCliente(Guid id, RequestEditarClienteDTO clienteDto)
    {
        try
        {
            var edited = await _serviceCliente.EditarCliente(id, clienteDto);
            
            if (edited)
            {
                return Created();
            }
        }
        catch (Exception)
        {
            return BadRequest("Os campos CPF e Email devem ser únicos");
        }

        return BadRequest("A idade do cliente deve ser maior que 18 anos.");
    }
}