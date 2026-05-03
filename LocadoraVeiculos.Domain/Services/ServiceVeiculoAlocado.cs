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

        public async Task AdicionarVeiculoAlocado(RequestAdicionarVeiculoAlocadoDTO veiculoAlocadoDto)
        {
            if (veiculoAlocadoDto.DataPrevDevol <= veiculoAlocadoDto.DataRetirada)
                throw new InvalidOperationException("A data prevista de devolução não pode ser antes da data de retirada.");

            bool veiculoDisponivel = await _veiculoAlocado.VerificarVeiculoLocacaoAtiva(veiculoAlocadoDto.PlacaVeiculo);

            bool clienteDisponivel = await _veiculoAlocado.VerificarClienteAtivo(veiculoAlocadoDto.ClienteId);

            if (!veiculoDisponivel || !clienteDisponivel)
                throw new InvalidOperationException("O veículo e o cliente precisa estar disponíveis para serem alocados.");

            await _veiculoAlocado.AdicionarVeiculoAlocado(veiculoAlocadoDto);
        }

        public async Task CancelarVeiculoAlocado(Guid id)
        {
            var locacao = await _veiculoAlocado.ListarVeiculoAlocadoPorId(id);

            if (locacao == null)
                throw new NullReferenceException("Nenhuma locação encontrada.");
            
            TimeZoneInfo brasiliaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            
            DateTime dataBrasilia = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brasiliaTimeZone);

            if (dataBrasilia >= locacao.DataRetirada)
                throw new InvalidOperationException("A locação não pode ser cancelada, pois, o veículo esta na data de retirada ou após a data de retirada.");

            await _veiculoAlocado.CancelarVeiculoAlocado(id);
        }

        public async Task DarBaixaVeiculoAlocado(Guid id)
        {
            var locacao = await _veiculoAlocado.ListarVeiculoAlocadoPorId(id);

            if (locacao == null)
                throw new NullReferenceException("Nenhuma locação encontrada.");

            TimeZoneInfo brasiliaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            
            DateTime dataBrasilia = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brasiliaTimeZone);

            locacao.DataDevolução = dataBrasilia;

            int diasUtilizados = (locacao.DataDevolução.Date - locacao.DataRetirada.Date).Days;

            if (diasUtilizados <= 0)
                diasUtilizados = 1;

            decimal precoDiaria = await _veiculoAlocado.ListarPrecoCategoriaVeiculo(locacao.PlacaVeiculo);
            locacao.ValorTotal = diasUtilizados * precoDiaria;

            locacao.Status = Status.Concluída;

            await _veiculoAlocado.DarBaixaVeiculoAlocado(id, locacao.ValorTotal, dataBrasilia);
        }
    }
}
