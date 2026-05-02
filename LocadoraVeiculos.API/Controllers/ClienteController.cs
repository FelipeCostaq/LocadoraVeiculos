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

    public ClienteController(InterfaceClienteApp interfaceClienteApp)
    {
        _interfaceClienteApp = interfaceClienteApp;
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
            await _interfaceClienteApp.AdicionarCliente(clienteDto);

            return Created();
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }
    
    [HttpPut("edit")]
    public async Task<IActionResult> EditarCliente(Guid id, RequestEditarClienteDTO clienteDto)
    {
        try
        {
            await _interfaceClienteApp.EditarCliente(id, clienteDto);
            
            return Created();
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpDelete]
    public async Task<IActionResult> ExcluirCliente(Guid id)
    {
        try
        {
            await _interfaceClienteApp.ExcluirCliente(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }
}