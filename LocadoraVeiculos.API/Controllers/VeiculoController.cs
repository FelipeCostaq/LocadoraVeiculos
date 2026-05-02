using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Application.Interfaces;
using LocadoraVeiculos.Domain.Services;
using LocadoraVeiculos.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraVeiculos.API.Controllers;

[ApiController]
[Route("veiculos")]
public class VeiculoController : ControllerBase
{
    public readonly InterfaceVeiculoApp _interfaceVeiculoApp;

    public VeiculoController(InterfaceVeiculoApp interfaceVeiculo)
    {
        _interfaceVeiculoApp = interfaceVeiculo;
    }

    [HttpGet]
    public async Task<IActionResult> ListarVeiculos()
    {
        var veiculos = await _interfaceVeiculoApp.ListarVeiculos();

        return Ok(veiculos);
    }

    [HttpGet("disponivel")]
    public async Task<IActionResult> ListarVeiculosDisponiveis()
    {
        var veiculos = await _interfaceVeiculoApp.ListarVeiculosDisponiveis();
        
        return Ok(veiculos);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AdicionarVeiculo(RequestAdicionarVeiculoDTO veiculoDto)
    {
        try
        {
            await _interfaceVeiculoApp.AdicionarVeiculo(veiculoDto);

            return Created();
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> EditarVeiculo(string placa, RequestEditarVeiculoDTO veiculoDto)
    {
        try
        {
            await _interfaceVeiculoApp.EditarVeiculo(placa, veiculoDto);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }
}