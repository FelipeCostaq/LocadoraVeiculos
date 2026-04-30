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
            var created = await _serviceCategoriaVeiculo.AdicionarCategoriaVeiculo(categoriaVeiculoDto);
            
            if (created)
            {
                return Created();
            }
        }
        catch (Exception)
        {
            return BadRequest("O nome deve ser único.");
        }

        return BadRequest("O valor da diária deve ser igual ou maior que 1.");
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> EditarCategoriaVeiculo(Guid id, RequestEditarCategoriaVeiculoDTO categoriaVeiculoDto)
    {
        try
        {
            var edited = await _serviceCategoriaVeiculo.EditarCategoriaVeiculo(id, categoriaVeiculoDto);
            
            if (edited)
            {
                return Created();
            }
        }
        catch (Exception)
        {
            return BadRequest("O nome deve ser único.");
        }

        return BadRequest("O valor da diária deve ser igual ou maior que 1.");
    }
}