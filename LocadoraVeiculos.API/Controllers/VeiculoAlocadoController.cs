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
            try
            {
                var created = await _serviceVeiculoAlocado.AdicionarVeiculoAlocado(veiculoAlocadoDto);

                if (created)
                    return Ok(veiculoAlocadoDto);
            }
            catch (Exception)
            {
                return BadRequest("O id do cliente e a placa do veículo devem ser válidos.");
            }

            return BadRequest("A data de devolução deve ser posterior a data de retirada. O veículo precisa estar disponível para ser alocado. O cliente e a placa precisam estar ativos");
        }

        [HttpPut("darbaixa")]
        [Authorize]
        public async Task<IActionResult> DarBaixaVeiculoAlocado(Guid id)
        {
            try
            {
                var created = await _serviceVeiculoAlocado.DarBaixaVeiculoAlocado(id);

                if (created)
                    return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return NotFound();
        }

        [HttpPut("cancelar")]
        [Authorize]
        public async Task<IActionResult> CancelarVeiculoAlocado(Guid id)
        {
            try
            {
                var created = await _serviceVeiculoAlocado.CancelarVeiculoAlocado(id);

                if (created)
                    return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return BadRequest("O cancelamento só pode ser feito caso a data de retirada não tenha chegado ainda.");
        }
    }
}
