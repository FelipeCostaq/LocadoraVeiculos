using LocadoraVeiculos.Domain.Interfaces.InterfaceCliente;
using LocadoraVeiculos.Domain.Interfaces.InterfaceServices;
using LocadoraVeiculos.Domain.Interfaces.InterfaceVeiculoAlocado;
using LocadoraVeiculos.Entities.DTOs;
using LocadoraVeiculos.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocadoraVeiculos.Domain.Services
{
    public class ServiceVeiculoAlocado : IServiceVeiculoAlocado
    {
        private readonly IVeiculoAlocado _veiculoAlocado;

        public ServiceVeiculoAlocado(IVeiculoAlocado veiculoAlocado)
        {
            _veiculoAlocado = veiculoAlocado;
        }

        public async Task<bool> AdicionarVeiculoAlocado(RequestAdicionarVeiculoAlocadoDTO veiculoAlocadoDto)
        {
            if (veiculoAlocadoDto.DataPrevDevol <= veiculoAlocadoDto.DataRetirada)
                return false;

            bool veiculoDisponivel = await _veiculoAlocado.VerificarVeiculoLocacaoAtiva(veiculoAlocadoDto.PlacaVeiculo);

            bool clienteDisponivel = await _veiculoAlocado.VerificarClienteAtivo(veiculoAlocadoDto.ClienteId);

            if (!veiculoDisponivel || !clienteDisponivel)
                return false;


            await _veiculoAlocado.AdicionarVeiculoAlocado(veiculoAlocadoDto);

            return true;
        }

        public async Task<bool> CancelarVeiculoAlocado(Guid id)
        {
            var locacao = await _veiculoAlocado.ListarVeiculoAlocadoPorId(id);

            if (locacao == null)
                return false;

            if (DateTime.Now >= locacao.DataRetirada)
                return false;

            await _veiculoAlocado.CancelarVeiculoAlocado(id);

            return true;
        }

        public async Task<bool> DarBaixaVeiculoAlocado(Guid id)
        {
            var locacao = await _veiculoAlocado.ListarVeiculoAlocadoPorId(id);

            if (locacao == null)
                return false;

            locacao.DataDevolução = DateTime.Now;

            int diasUtilizados = (locacao.DataDevolução.Date - locacao.DataRetirada.Date).Days;

            if (diasUtilizados <= 0)
                diasUtilizados = 1;

            decimal precoDiaria = await _veiculoAlocado.ListarPrecoCategoriaVeiculo(locacao.PlacaVeiculo);
            locacao.ValorTotal = diasUtilizados * precoDiaria;

            locacao.Status = Status.Concluída;

            await _veiculoAlocado.DarBaixaVeiculoAlocado(id, locacao.ValorTotal);

            return true;
        }
    }
}
