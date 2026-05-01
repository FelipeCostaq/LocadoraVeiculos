using LocadoraVeiculos.Application.Interfaces;
using LocadoraVeiculos.Domain.Interfaces.InterfaceServices;
using LocadoraVeiculos.Domain.Interfaces.InterfaceVeiculo;
using LocadoraVeiculos.Domain.Interfaces.InterfaceVeiculoAlocado;
using LocadoraVeiculos.Entities.DTOs;
using LocadoraVeiculos.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocadoraVeiculos.Application.OpenApp
{
    public class AppVeiculoAlocado : InterfaceVeiculoAlocadoApp
    {
        private readonly IVeiculoAlocado _iveiculoAlocado;
        private readonly IServiceVeiculoAlocado _iserviceVeiculoAlocado;

        public AppVeiculoAlocado(IVeiculoAlocado iveiculoAlocado, IServiceVeiculoAlocado iserviceVeiculoAlocado)
        {
            _iveiculoAlocado = iveiculoAlocado;
            _iserviceVeiculoAlocado = iserviceVeiculoAlocado;
        }

        public async Task AdicionarVeiculoAlocado(RequestAdicionarVeiculoAlocadoDTO veiculoAlocadoDTO)
        {
            await _iserviceVeiculoAlocado.AdicionarVeiculoAlocado(veiculoAlocadoDTO);
        }

        public async Task DarBaixaVeiculoAlocado(Guid id)
        {
            await _iserviceVeiculoAlocado.DarBaixaVeiculoAlocado(id);
        }

        public async Task<List<VeiculoAlocado>> ListarVeiculosAlocados()
        {
            return await _iveiculoAlocado.ListarVeiculosAlocados();
        }

        public async Task<List<VeiculoAlocado>> ListarVeiculosAlocadosDisponibilidade()
        {
            return await _iveiculoAlocado.ListarVeiculosAlocadosDisponibilidade();
        }
    }
}
