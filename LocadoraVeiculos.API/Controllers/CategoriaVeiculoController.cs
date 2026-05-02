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

    public CategoriaVeiculoController(InterfaceCategoriaVeiculoApp interfaceCategoriaVeiculoApp)
    {
        _interfaceCategoriaVeiculoApp = interfaceCategoriaVeiculoApp;
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
            await _interfaceCategoriaVeiculoApp.AdicionarCategoriaVeiculo(categoriaVeiculoDto);

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
            await _interfaceCategoriaVeiculoApp.EditarCategoriaVeiculo(id, categoriaVeiculoDto);
            
            return Created();
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }
}