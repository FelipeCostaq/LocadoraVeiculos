using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Application.Interfaces;
using LocadoraVeiculos.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LocadoraVeiculos.Entities.DTOs;

namespace LocadoraVeiculos.API.Controllers
{
    [ApiController]
    public class VeiculoAlocadoController : ControllerBase
    {
        public readonly InterfaceVeiculoAlocadoApp _interfaceVeiculoAlocadoApp;
        public readonly ServiceVeiculoAlocado _serviceVeiculoAlocado;

        public VeiculoAlocadoController(InterfaceVeiculoAlocadoApp interfaceVeiculoAlocadoApp, ServiceVeiculoAlocado serviceVeiculoAlocado)
        {
            _interfaceVeiculoAlocadoApp = interfaceVeiculoAlocadoApp;
            _serviceVeiculoAlocado = serviceVeiculoAlocado;
        }

        [HttpGet]
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
            // Implementar o service
        }

        [HttpPut("darbaixa")]
        [Authorize]
        public async Task<IActionResult> DarBaixaVeiculoAlocado(Guid id)
        {
            // Implementar o service
        }
    }
}
