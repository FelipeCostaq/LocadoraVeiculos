using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocadoraVeiculos.Domain.Interfaces.InterfaceServices
{
    public interface IServiceVeiculoAlocado
    {
        public Task AdicionarVeiculoAlocado(RequestAdicionarVeiculoAlocadoDTO veiculoAlocadoDto);

        public Task DarBaixaVeiculoAlocado(Guid id);

        public Task CancelarVeiculoAlocado(Guid id);
    }
}
