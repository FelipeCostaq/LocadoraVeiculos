using LocadoraVeiculos.Domain.Interfaces.InterfaceVeiculoAlocado;
using LocadoraVeiculos.Entities.DTOs;
using LocadoraVeiculos.Entities.Entities;
using LocadoraVeiculos.Entities.Enums;
using LocadoraVeiculos.Infrastructure.Data;
using LocadoraVeiculos.Infrastructure.Repositories.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocadoraVeiculos.Infrastructure.Repositories
{
    public class RepositoryVeiculoAlocado : RepositoryGenerics<VeiculoAlocado>, IVeiculoAlocado
    {
        private readonly DbContextOptions<LocadoraContext> _options;

        public RepositoryVeiculoAlocado(DbContextOptions<LocadoraContext> options)
        {
            _options = options;
        }

        public async Task AdicionarVeiculoAlocado(RequestAdicionarVeiculoAlocadoDTO veiculoDto)
        {
            using (var data = new LocadoraContext(_options))
            {
                VeiculoAlocado veiculoAlocado = new VeiculoAlocado
                {
                    ClienteId = veiculoDto.ClienteId,
                    PlacaVeiculo = veiculoDto.PlacaVeiculo,
                    DataRetirada = veiculoDto.DataRetirada,
                    DataPrevDevol = veiculoDto.DataPrevDevol,
                    DataDevolução = veiculoDto.DataDevolução
                };

                await data.VeiculosAlocados.AddAsync(veiculoAlocado);

                Veiculo veiculo = await data.Veiculos.FindAsync(veiculoDto.PlacaVeiculo);

                veiculo.Disponivel = false;

                await data.SaveChangesAsync();
            }
        }

        public async Task DarBaixaVeiculoAlocado(Guid id)
        {

            using (var data = new LocadoraContext(_options))
            {
                VeiculoAlocado veiculoAlocado = await data.VeiculosAlocados.FindAsync(id);

                veiculoAlocado.DataDevolução = DateTime.Now;
                veiculoAlocado.Status = Status.Concluída;

                Veiculo veiculo = await data.Veiculos.FindAsync(veiculoAlocado.PlacaVeiculo);

                veiculo.Disponivel = true;

                await data.SaveChangesAsync();
            }
        }

        public async Task<List<VeiculoAlocado>> ListarVeiculosAlocados()
        {
            using (var data = new LocadoraContext(_options))
            {
                return await data.VeiculosAlocados.AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<VeiculoAlocado>> ListarVeiculosAlocadosDisponibilidade()
        {
            using (var data = new LocadoraContext(_options))
            {

                return await data.VeiculosAlocados.Select(v => new VeiculoAlocado()
                {
                    PlacaVeiculo = v.PlacaVeiculo,
                    Status = v.Status
                }).AsNoTracking().ToListAsync();
            }
        }
    }
}
