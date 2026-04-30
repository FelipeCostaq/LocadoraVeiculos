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

        public Task DarBaixaVeiculoAlocado(Guid id);

        public Task<List<VeiculoAlocado>> ListarVeiculosAlocados();

        public Task<List<VeiculoAlocado>> ListarVeiculosAlocadosDisponibilidade();
    }
}
