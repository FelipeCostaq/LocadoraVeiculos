using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Entities.DTOs;
using LocadoraVeiculos.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocadoraVeiculos.Application.Interfaces
{
    public interface InterfaceVeiculoAlocadoApp
    {
        Task AdicionarVeiculoAlocado(RequestAdicionarVeiculoAlocadoDTO veiculoAlocadoDTO);
        Task DarBaixaVeiculoAlocado(Guid id);
        Task CancelarVeiculoAlocado(Guid id);
        Task<List<VeiculoAlocado>> ListarVeiculosAlocados();
        Task<List<VeiculoAlocado>> ListarVeiculosAlocadosDisponibilidade();
    }
}
