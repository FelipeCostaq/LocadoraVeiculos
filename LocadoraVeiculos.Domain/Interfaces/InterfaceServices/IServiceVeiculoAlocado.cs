using LocadoraVeiculos.Application.DTOs;
using LocadoraVeiculos.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocadoraVeiculos.Domain.Interfaces.InterfaceServices
{
    public interface IServiceVeiculoAlocado
    {
        public Task<bool> AdicionarVeiculoAlocado(RequestAdicionarVeiculoAlocadoDTO veiculoDto);

        public Task<bool> DarBaixaVeiculoAlocado(Guid id);
    }
}
