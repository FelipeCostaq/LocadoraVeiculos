using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Entities.DTOs;
using LocadoraVeiculos.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocadoraVeiculos.Domain.Interfaces.InterfaceVeiculoAlocado
{
    public interface IVeiculoAlocado
    {
        public Task AdicionarVeiculoAlocado(RequestAdicionarVeiculoAlocadoDTO veiculoDto);

        public Task DarBaixaVeiculoAlocado(Guid id, decimal valorTotalCalculado, DateTime dataDevolucao);

        public Task<List<VeiculoAlocado>> ListarVeiculosAlocados();

        public Task<List<VeiculoAlocado>> ListarVeiculosAlocadosDisponibilidade();

        public Task<bool> VerificarVeiculoLocacaoAtiva(string placa);

        public Task<bool> VerificarClienteAtivo(Guid id);

        public Task<VeiculoAlocado> ListarVeiculoAlocadoPorId(Guid id);

        public Task<decimal> ListarPrecoCategoriaVeiculo(string placa);

        public Task CancelarVeiculoAlocado(Guid id);
    }
}
