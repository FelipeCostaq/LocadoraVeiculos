using LocadoraVeiculos.Application.Interfaces;
using LocadoraVeiculos.Domain.Interfaces.InterfaceServices;
using LocadoraVeiculos.Domain.Interfaces.InterfaceVeiculo;
using LocadoraVeiculos.Entities.DTOs;
using LocadoraVeiculos.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocadoraVeiculos.Application.OpenApp
{
    public class AppVeiculoAlocado : InterfaceVeiculoAlocadoApp
    {
        private readonly IVeiculoAlocado _iveiculo;
        private readonly IServiceVeiculo _iserviceVeiculo;

        public AppVeiculo(IVeiculo iveiculo, IServiceVeiculo iserviceVeiculo)
        {
            _iveiculo = iveiculo;
            _iserviceVeiculo = iserviceVeiculo;
        }

        public Task AdicionarVeiculo(RequestAdicionarVeiculoAlocadoDTO veiculoDto)
        {
            throw new NotImplementedException();
        }

        public Task DarBaixaVeiculoAlocado(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Veiculo>> ListarVeiculosAlocados()
        {
            throw new NotImplementedException();
        }

        public Task<List<Veiculo>> ListarVeiculosAlocadosDisponibilidade()
        {
            throw new NotImplementedException();
        }
    }
}
