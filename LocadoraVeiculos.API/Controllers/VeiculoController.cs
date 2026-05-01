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
    public readonly ServiceVeiculo _serviceVeiculo;

    public VeiculoController(InterfaceVeiculoApp interfaceVeiculo, ServiceVeiculo serviceVeiculo)
    {
        _interfaceVeiculoApp = interfaceVeiculo;
        _serviceVeiculo = serviceVeiculo;
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
            var created = await _serviceVeiculo.AdicionarVeiculo(veiculoDto);

            if (created)
            {
                return Created();
            }
        }
        catch (Exception)
        {
            return BadRequest("A placa do veículo precisa ser única.");
        }

        return BadRequest($"O ano do veículo precisa estar entre 1990 e {DateTime.Now.Year}");
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> EditarVeiculo(string placa, RequestEditarVeiculoDTO veiculoDto)
    {
        try
        {
            var edited = await _serviceVeiculo.EditarVeiculo(placa, veiculoDto);

            if (edited)
            {
                return Ok();
            }
        }
        catch (Exception ex)
        {
            return BadRequest("A placa do veículo precisa ser única.");
        }

        return BadRequest($"Um veículo para ser editado precisa estar com estado Ativo igual a false e o ano do veículo precisa estar entre 1990 e {DateTime.Now.Year}. Para alterar a categoria de um veículo ele não pode estar em uma locação.");
    }
}