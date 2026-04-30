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
        Task AdicionarVeiculo(RequestAdicionarVeiculoAlocadoDTO veiculoDto);
        Task DarBaixaVeiculoAlocado(Guid id);
        Task<List<Veiculo>> ListarVeiculosAlocados();
        Task<List<Veiculo>> ListarVeiculosAlocadosDisponibilidade();
    }
}
