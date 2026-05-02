using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Application.Interfaces;
using LocadoraVeiculos.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LocadoraVeiculos.Entities.DTOs;

namespace LocadoraVeiculos.API.Controllers
{
    [ApiController]
    [Route("veiculoalocado")]
    public class VeiculoAlocadoController : ControllerBase
    {
        public readonly InterfaceVeiculoAlocadoApp _interfaceVeiculoAlocadoApp;

        public VeiculoAlocadoController(InterfaceVeiculoAlocadoApp interfaceVeiculoAlocadoApp)
        {
            _interfaceVeiculoAlocadoApp = interfaceVeiculoAlocadoApp;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListarVeiculosAlocados()
        {
            var veiculosAlocados = await _interfaceVeiculoAlocadoApp.ListarVeiculosAlocados();

            return Ok(veiculosAlocados);
        }

        [HttpGet("disponibilidade")]
        public async Task<IActionResult> ListarVeiculosAlocadosDisponibilidade()
        {
            var veiculosAlocadosDisponibilidade = await _interfaceVeiculoAlocadoApp.ListarVeiculosAlocadosDisponibilidade();

            return Ok(veiculosAlocadosDisponibilidade);
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AdicionarVeiculoAlocado(RequestAdicionarVeiculoAlocadoDTO veiculoAlocadoDto)
        {
            try
            {
                await _interfaceVeiculoAlocadoApp.AdicionarVeiculoAlocado(veiculoAlocadoDto);

                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("darbaixa")]
        [Authorize]
        public async Task<IActionResult> DarBaixaVeiculoAlocado(Guid id)
        {
            try
            {
                await _interfaceVeiculoAlocadoApp.DarBaixaVeiculoAlocado(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("cancelar")]
        [Authorize]
        public async Task<IActionResult> CancelarVeiculoAlocado(Guid id)
        {
            try
            {
                await _interfaceVeiculoAlocadoApp.CancelarVeiculoAlocado(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}
