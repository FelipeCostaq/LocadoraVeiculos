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
    [Authorize]
    public async Task<IActionResult> AdicionarCliente(RequestAdicionarClienteDTO clienteDto)
    {
        try
        {
            await _serviceCliente.AdicionarCliente(clienteDto);

            return Created();
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }
    
    [HttpPut("edit")]
    [Authorize]
    public async Task<IActionResult> EditarCliente(Guid id, RequestEditarClienteDTO clienteDto)
    {
        try
        {
            await _serviceCliente.EditarCliente(id, clienteDto);
            
            return Created();
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> ExcluirCliente(Guid id)
    {
        try
        {
            await _serviceCliente.ExcluirCliente(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }
}