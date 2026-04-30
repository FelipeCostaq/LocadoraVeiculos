using LocadoraVeiculos.Domain.Interfaces.InterfaceCliente;
using LocadoraVeiculos.Domain.Interfaces.InterfaceServices;
using LocadoraVeiculos.Domain.Interfaces.InterfaceVeiculoAlocado;
using LocadoraVeiculos.Entities.DTOs;
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

        public Task<bool> AdicionarVeiculoAlocado(RequestAdicionarVeiculoAlocadoDTO veiculoDto)
        {
            if (veiculoDto.DataDevolução > veiculoDto.DataRetirada)
            {

            }
        }

        public Task<bool> DarBaixaVeiculoAlocado(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
