using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Application.Interfaces;
using LocadoraVeiculos.Application.OpenApp;
using LocadoraVeiculos.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraVeiculos.API.Controllers;

[ApiController]
[Route("categoria")]
public class CategoriaVeiculoController : ControllerBase
{
    public readonly InterfaceCategoriaVeiculoApp _interfaceCategoriaVeiculoApp;
    public readonly ServiceCategoriaVeiculo _serviceCategoriaVeiculo;

    public CategoriaVeiculoController(InterfaceCategoriaVeiculoApp interfaceCategoriaVeiculoApp, ServiceCategoriaVeiculo serviceCategoriaVeiculo)
    {
        _interfaceCategoriaVeiculoApp = interfaceCategoriaVeiculoApp;
        _serviceCategoriaVeiculo = serviceCategoriaVeiculo;
    }

    [HttpGet]
    public async Task<IActionResult> ListarCategoriasVeiculo()
    {
        var categoriasVeiculo = await _interfaceCategoriaVeiculoApp.ListarCategoriasVeiculo();
        
        return Ok(categoriasVeiculo);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AdicionarCategoriaVeiculo(RequestAdicionarCategoriaVeiculoDTO categoriaVeiculoDto)
    {
        try
        {
            await _serviceCategoriaVeiculo.AdicionarCategoriaVeiculo(categoriaVeiculoDto);

            return Created();
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> EditarCategoriaVeiculo(Guid id, RequestEditarCategoriaVeiculoDTO categoriaVeiculoDto)
    {
        try
        {
            await _serviceCategoriaVeiculo.EditarCategoriaVeiculo(id, categoriaVeiculoDto);
            
            return Created();
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }
}